using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Proxy.Http;
using Microsoft.AspNetCore.Http;
using Core.Proxy.Serialization;
using System.Globalization;

namespace Core.Proxy
{
    public class ProxyManager
    {
        private static readonly short INVALID_TOKEN_CODE = 33;

        private static AllInOneRequest lastPostRequest;
        private static string lastPostResponse;

        //IAuthenticationManager authManager;
        private readonly Serializer _serializer;
        private static ProxyManager _instance;

        private HttpContext context { get; set; }

        public static ProxyManager GetInstance(IHttpContextAccessor contextAccessor)
        {
            if (_instance == null)
                _instance = new ProxyManager(contextAccessor);
            return _instance;
        }

        private ProxyManager(IHttpContextAccessor contextAccessor)
        {
            _serializer = new Serializer();
            context = contextAccessor.HttpContext;
            //authManager = AuthenticationManager.Instance;
        }

        public AllInOneResponse DoRequest(AllInOneRequest request)
        {
            var builder = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(request.ServerUrl))
                    throw new AllInOneException("Invalid server");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(request.ServerUrl);
                if (request.IsAuthorized)
                {
                    httpWebRequest.Headers["Authorization"] = $"{Constants.Headers.Bearer} {request.AccessToken}";
                }
                foreach (var header in request.Headers)
                {
                    httpWebRequest.Headers[header.Key] = header.Value;
                }
                //AddDefaultRequestHeaders(ref httpWebRequest);

                httpWebRequest.Method = request.Method.ToString();
                builder.AppendLine("Request at " + GetDateNowString());
                builder.AppendLine($"{request.ServerUrl} [{request.Method}]");

                builder.Append("Headers: ");
                foreach (var headerKey in httpWebRequest.Headers.AllKeys)
                {
                    builder.AppendLine($"[{headerKey}:{httpWebRequest.Headers[headerKey]}] ");
                }
                builder.AppendLine();

                if (request.Body != null)
                {
                    builder.Append("Body: ");
                    if (request.IsFormUrlEncoded)
                    {
                        httpWebRequest.ContentType = Constants.Headers.FormUrlEncodedContentType;
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
                        {
                            builder.AppendLine(request.Body.ToString());
                            streamWriter.Write(request.Body);
                            streamWriter.Flush();
                        }
                        //httpWebResponse = await httpWebRequest.PostAsync(request.ServerUrl, new FormUrlEncodedContent(request.Body as Dictionary<string, string>));

                    }
                    else // JSON Request.
                    {
                        httpWebRequest.ContentType = Constants.Headers.JsonContentType;
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
                        {
                            var json = _serializer.SerializeObject(request.Body);
                            builder.AppendLine(json);
                            streamWriter.Write(json);
                            streamWriter.Flush();
                        }
                    }
                }
                var responseString = string.Empty; //await httpWebResponse.Content.ReadAsStringAsync();
                if (request.Method == HttpMethod.Post && !request.AllowDuplicate)
                {
                    if (lastPostRequest != null && request.ServerUrl == lastPostRequest.ServerUrl &&
                        request.Body == lastPostRequest.Body &&
                        request.CreatedDate.Subtract(lastPostRequest.CreatedDate).Seconds < 3)
                    {
                        responseString = lastPostResponse;
                        builder.Insert(0, "[DUPLICATED] ");
                    }

                    lastPostRequest = request;
                }

                Logger.LoggerManager.LogRequest(builder);
                builder.Clear();
                if (string.IsNullOrEmpty(responseString))
                {
                    try
                    {
                        httpWebRequest.Timeout = 300000;
                        var httpResponse = httpWebRequest.GetResponseAsync().Result;

                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            responseString = streamReader.ReadToEnd();
                        }
                    }
                    catch (AggregateException ex)
                    {
                        responseString = ex.Message;
                        if (ex.InnerException.GetType() == typeof(WebException))
                        {
                            WebException wex = (WebException)ex.InnerException;
                            if (wex.Response != null)
                            {
                                using (var errorResponse = (HttpWebResponse)wex.Response)
                                {
                                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                                    {
                                        responseString = reader.ReadToEnd();
                                        //TODO: use JSON.net to parse this string and look at the error message
                                    }
                                }
                            }
                        }
                    }
                    catch (WebException wex)
                    {
                        if (wex.Response != null)
                        {
                            using (var errorResponse = (HttpWebResponse)wex.Response)
                            {
                                using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                                {
                                    responseString = reader.ReadToEnd();
                                    //TODO: use JSON.net to parse this string and look at the error message
                                }
                            }
                        }
                    }
                }

                builder.AppendLine("Response at " + GetDateNowString());
                builder.AppendLine(responseString);

                AllInOneResponse TObject = new AllInOneResponse();
                try
                {
                    TObject = _serializer.DeserializeObject<AllInOneResponse>(responseString);
                }
                catch (Exception ex)
                {
                    builder.AppendLine("Exception: " + ex.Message);
                }
                finally
                {
                    TObject.ResponseString = responseString;
                    builder.AppendLine("*************************************************************");
                    builder.AppendLine();
                    Logger.LoggerManager.LogRequest(builder);
                }

                if (TObject.HasError) // invalid response.
                {
                    if (TObject.code == INVALID_TOKEN_CODE)
                    {
                        if (request.RefreshTokenDelegate != null)
                        {
                            var result = request.RefreshTokenDelegate();
                            if (string.IsNullOrWhiteSpace(result))
                                throw new InvalidTokenException();

                            request.AccessToken = result;
                            TObject = DoRequest(request);
                        }
                        else
                        {
                            //return TObject;
                            throw new InvalidTokenException();
                        }
                    }

                    // if code = expired/invalid token
                    //throw new ExpiredTokenException();

                    //TODO: throw AllInOne Exception after handling any exception.
                    //throw new AllInOneException(new Error { Code = TObject.code.ToString(), Message = TObject.message }, null);
                }
                else if (request.Method == HttpMethod.Post)
                {
                    lastPostResponse = responseString;
                }

                return TObject;
            }
            catch (ExpiredTokenException)
            {
                //await authManager.RefreshToken();

                // re generate the old request again.
                //return await DoRequest(request);
                return null;
            }
            finally
            {
            }
            // TODO: Handle any exception
            //catch (AllInOneException AllInOneException)
            //{
            //    throw;
            //} 
        }

        public Stream DoDownloadRequest(AllInOneRequest request)
        {
            var builder = new StringBuilder();
            try
            {
                if (string.IsNullOrEmpty(request.ServerUrl))
                    throw new AllInOneException("Invalid server");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(request.ServerUrl);
                if (request.IsAuthorized)
                    httpWebRequest.Headers["Authorization"] = $"{Constants.Headers.Bearer} {request.AccessToken}";

                foreach (var header in request.Headers)
                    httpWebRequest.Headers[header.Key] = header.Value;

                //AddDefaultRequestHeaders(ref httpWebRequest);

                httpWebRequest.Method = request.Method.ToString();
                builder.AppendLine("Request at " + GetDateNowString());
                builder.AppendLine($"{request.ServerUrl} [{request.Method}]");

                builder.Append("Headers: ");
                foreach (var headerKey in httpWebRequest.Headers.AllKeys)
                {
                    builder.AppendLine($"[{headerKey}:{httpWebRequest.Headers[headerKey]}] ");
                }
                builder.AppendLine();

                if (request.Body != null)
                {
                    if (request.IsFormUrlEncoded)
                    {
                        httpWebRequest.ContentType = Constants.Headers.FormUrlEncodedContentType;
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
                        {
                            builder.AppendLine(request.Body.ToString());
                            streamWriter.Write(request.Body);
                            streamWriter.Flush();
                        }
                        //httpWebResponse = await httpWebRequest.PostAsync(request.ServerUrl, new FormUrlEncodedContent(request.Body as Dictionary<string, string>));
                    }
                    else // JSON Request.
                    {
                        httpWebRequest.ContentType = Constants.Headers.JsonContentType;
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
                        {
                            var json = _serializer.SerializeObject(request.Body);
                            builder.AppendLine(json);
                            streamWriter.Write(json);
                            streamWriter.Flush();
                        }
                    }
                }

                Logger.LoggerManager.LogRequest(builder);
                builder.Clear();

                Stream responseStream = null; //await httpWebResponse.Content.ReadAsStringAsync();
                try
                {
                    var httpResponse = httpWebRequest.GetResponseAsync().Result as HttpWebResponse;
                    responseStream = httpResponse.GetResponseStream();
                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                responseStream = null;
                                //TODO: use JSON.net to parse this string and look at the error message
                            }
                        }
                    }
                }

                return responseStream;
            }
            catch (ExpiredTokenException)
            {
                //await authManager.RefreshToken();

                // re generate the old request again.
                //return await DoRequest(request);
                return null;
            }
            finally
            {
                builder.AppendLine("*************************************************************");
                builder.AppendLine();
                Logger.LoggerManager.LogRequest(builder);
            }
            // TODO: Handle any exception
            //catch (AllInOneException AllInOneException)
            //{
            //    throw;
            //}
        }

        public async Task<AllInOneResponse> DoRequestAsync(AllInOneRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ServerUrl))
                    throw new AllInOneException("Invalid server");

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(request.ServerUrl);
                if (request.IsAuthorized)
                    httpWebRequest.Headers["Authorization"] = $"{Constants.Headers.Bearer} {request.AccessToken}";

                //AddDefaultRequestHeaders(ref httpWebRequest);

                if (request.Body != null)
                {
                    httpWebRequest.Method = request.Method.ToString();
                    if (request.IsFormUrlEncoded)
                    {
                        httpWebRequest.ContentType = Constants.Headers.FormUrlEncodedContentType;
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
                        {
                            streamWriter.Write(request.Body);
                            streamWriter.Flush();
                            streamWriter.Dispose();
                        }
                        //httpWebResponse = await httpWebRequest.PostAsync(request.ServerUrl, new FormUrlEncodedContent(request.Body as Dictionary<string, string>));
                    }
                    else // JSON Request.
                    {
                        httpWebRequest.ContentType = Constants.Headers.JsonContentType;
                        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStreamAsync().Result))
                        {
                            var json = _serializer.SerializeObject(request.Body);
                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Dispose();
                        }
                    }
                }
                else
                    httpWebRequest.Method = request.Method.ToString();

                var responseString = string.Empty; //await httpWebResponse.Content.ReadAsStringAsync();
                try
                {
                    var httpResponse = await httpWebRequest.GetResponseAsync() as HttpWebResponse;

                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        responseString = streamReader.ReadToEnd();
                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                responseString = reader.ReadToEnd();
                                //TODO: use JSON.net to parse this string and look at the error message
                            }
                        }
                    }
                }

                var TObject = _serializer.DeserializeObject<AllInOneResponse>(responseString);
                TObject.ResponseString = responseString;

                if (TObject.HasError) // invalid response.
                {
                    // if code = expired/invalid token
                    //throw new ExpiredTokenException();

                    //TODO: throw AllInOne Exception after handling any exception.
                    //throw new AllInOneException(new Error { Code = TObject.code.ToString(), Message = TObject.message }, null);
                }

                return TObject;
            }
            catch (ExpiredTokenException)
            {
                //await authManager.RefreshToken();

                // re generate the old request again.
                //return await DoRequest(request);
                return null;
            }
            // TODO: Handle any exception
            //catch (AllInOneException AllInOneException)
            //{
            //    throw;
            //}

        }
        public AllInOneResponse<T> DoRequest<T>(AllInOneRequest request)
        {
            var response = DoRequest(request);
            if (response.HasError)
                return new AllInOneResponse<T> { code = response.code, message = response.message, ResponseString = response.ResponseString };

            try
            {
                var result = _serializer.DeserializeObject<AllInOneResponse<T>>(response.ResponseString);
                result.ResponseString = response.ResponseString;
                return result;
            }
            catch (Exception ex)
            {
                Logger.LoggerManager.LogException(ex, response.ResponseString);
                throw;
            }
        }

        public async Task<AllInOneResponse<T>> DoRequestAsync<T>(AllInOneRequest request)
        {
            var response = await DoRequestAsync(request);
            var TObject = _serializer.DeserializeObject<AllInOneResponse<T>>(response.ResponseString);
            return TObject;
        }

        //private void AddDefaultRequestHeaders(ref HttpWebRequest client)
        //{
        //    client.Headers["lang"] = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        //}

        public AllInOneResponse SendAsync(IFormFile file, string fileParamterName, string url, List<KeyValuePair<string, string>> headerList, List<KeyValuePair<string, string>> parameterList, string accessToken)
        {
            return SendAsync(new List<FileItem> { new FileItem { File = file, ParamterName = fileParamterName } }, url,
                headerList, parameterList, accessToken);
        }

        public AllInOneResponse SendAsync(List<FileItem> fileItems, string url, List<KeyValuePair<string, string>> headerList, List<KeyValuePair<string, string>> parameterList, string accessToken)
        {
            var builder = new StringBuilder();
            var content = new MultipartFormDataContent();
            var fileContentArray = new StreamContent[fileItems.Count];
            for (var i = 0; i < fileItems.Count; i++)
            {
                if (fileItems[i].File != null)
                {
                    //MemoryStream stream = new MemoryStream();
                    //fileItems[i].File.CopyTo(stream);
                    var stream = fileItems[i].File.OpenReadStream();
                    fileContentArray[i] = new StreamContent(stream)
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = fileItems[i].ParamterName,
                                FileName = DateTime.Now.Ticks.ToString()//$"\"{fileItems[i].File.FileName}\""
                            },
                            ContentType = new MediaTypeHeaderValue(fileItems[i].File.ContentType)
                        }
                    };
                    fileContentArray[i].LoadIntoBufferAsync().Wait();

                    content.Add(fileContentArray[i]);
                    builder.AppendLine("Request at " + GetDateNowString());

                    builder.AppendLine(url);
                    builder.AppendLine("FileParameterName =" + fileItems[i].ParamterName);

                    builder.AppendLine("File Content Type = " + fileItems[i].File.ContentType);
                }
            }

            if (parameterList.Count > 0)
            {
                builder.Append("Parameters = ");
                foreach (var item in parameterList)
                {
                    if (!string.IsNullOrWhiteSpace(item.Value))
                    {
                        var paramContent = new StringContent(item.Value);
                        paramContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = item.Key };
                        content.Add(paramContent);
                        builder.AppendFormat("{0}={1}&", item.Key, item.Value);
                    }
                }
                builder.AppendLine();
            }

            AllInOneResponse TObject = null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                if (headerList.Count > 0)
                {
                    builder.Append("Request Headers = ");
                    foreach (var item in headerList)
                    {
                        client.DefaultRequestHeaders.Add(item.Key, item.Value);
                        builder.AppendFormat("{0}={1}&", item.Key, item.Value);
                    }
                    builder.AppendLine();
                }

                client.DefaultRequestHeaders.Add("Authorization", $"{Constants.Headers.Bearer} {accessToken}");
                client.Timeout = new TimeSpan(3, 0, 0);
                try
                {
                    content.LoadIntoBufferAsync().Wait();
                    //client.PostAsync(url, content).Wait();
                    var response = client.PostAsync(url, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var cont = response.Content.ReadAsStringAsync();
                        builder.AppendLine("Response at " + GetDateNowString());
                        builder.AppendLine(cont.Result);
                        if (!string.IsNullOrWhiteSpace(cont.Result))
                        {
                            TObject = _serializer.DeserializeObject<AllInOneResponse>(cont.Result);
                            TObject.ResponseString = cont.Result;
                        }
                    }
                    else
                    {
                        builder.AppendLine("Response at " + GetDateNowString());
                        builder.AppendLine(response.ToString());
                        if (response.Content != null)
                        {
                            var cont = response.Content.ReadAsStringAsync();
                            if (!string.IsNullOrEmpty(cont.Result))
                            {
                                TObject = _serializer.DeserializeObject<AllInOneResponse>(cont.Result);
                                TObject.ResponseString = cont.Result;


                                builder.AppendLine("Response Content = " + cont.Result);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    var exceptionBuilder = new StringBuilder();

                    do
                    {
                        exceptionBuilder.AppendLine($"Exception = {ex.Message}");
                        ex = ex.InnerException;
                    } while (ex != null);

                    TObject = new AllInOneResponse { code = 500, message = exceptionBuilder.ToString() };
                    builder.AppendLine("Response at " + GetDateNowString());
                    builder.AppendLine(exceptionBuilder.ToString());
                }
                finally
                {
                    builder.AppendLine("*************************************************************");
                    builder.AppendLine();
                    Logger.LoggerManager.LogRequest(builder);
                }
            }

            return TObject;
        }

        public AllInOneResponse<T> SendAsync<T>(List<FileItem> fileItems, string url, List<KeyValuePair<string, string>> headerList, List<KeyValuePair<string, string>> parameterList, string accessToken)
        {
            var response = SendAsync(fileItems, url, headerList, parameterList, accessToken);
            AllInOneResponse<T> TObject = null;
            if (response != null && !response.HasError)
                TObject = _serializer.DeserializeObject<AllInOneResponse<T>>(response.ResponseString);
            else if (response != null && response.HasError)
                TObject = new AllInOneResponse<T>(response);

            return TObject;
        }

        public static string GetDateNowString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff", new CultureInfo("en-us"));
        }
    }
}
