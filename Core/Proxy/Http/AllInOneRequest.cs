using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Core.Exceptions;

namespace Core.Proxy.Http
{
    public class AllInOneRequest
    {
        public string ServerUrl { get; set; }

        public Dictionary<string, string> Headers { get; private set; }

        public HttpMethod Method { get; private set; }

        public bool IsAuthorized { get; private set; }

        public object Body { get; set; }

        public bool IsFormUrlEncoded { get; private set; }

        private IHttpContextAccessor _context { get; set; }

        public string AccessToken { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool AllowDuplicate { get; set; }

        private AllInOneRequest(IHttpContextAccessor context)
        {
            _context = context;
            Headers = new Dictionary<string, string>();
        }
        

        public AllInOneRequest(IHttpContextAccessor context, string serverUrl, string method, string accessToken, Func<string> refreshTokenDelegate = null, bool isAuthorized = false, bool isFormUrlEncoded = false) : this(context)
        {
            ServerUrl = serverUrl;
            Method = GetMethod(method);
            AccessToken = context.HttpContext.User.Identity.IsAuthenticated ? context.HttpContext.User.FindFirst("access_token")?.Value : accessToken; //accessToken;
            IsAuthorized = isAuthorized;
            IsFormUrlEncoded = isFormUrlEncoded;
            RefreshTokenDelegate = refreshTokenDelegate;
            if (IsAuthorized && string.IsNullOrEmpty(AccessToken))
                throw new AllInOneException("Invalid access token");
            CreatedDate = DateTime.Now;
        }

        public Func<string> RefreshTokenDelegate { get; set; }

        public string RefreshToken { get; set; }

        public AllInOneResponse GetUploadResponse(IFormFile file, string fileParamterName, List<KeyValuePair<string, string>> headerList, List<KeyValuePair<string, string>> parameterList)
        {
            return ProxyManager.GetInstance(_context).SendAsync(file, fileParamterName, ServerUrl, headerList, parameterList, AccessToken);
        }

        public AllInOneResponse<T> GetUploadResponse<T>(List<FileItem> fileItems, List<KeyValuePair<string, string>> headerList, List<KeyValuePair<string, string>> parameterList)
        {
            return ProxyManager.GetInstance(_context).SendAsync<T>(fileItems, ServerUrl, headerList, parameterList, AccessToken);
        }

        public AllInOneResponse GetResponse()
        {
            return ProxyManager.GetInstance(_context).DoRequest(this);
        }

        public async Task<AllInOneResponse> GetResponseAsync()
        {
            return await ProxyManager.GetInstance(_context).DoRequestAsync(this);
        }

        public async Task<AllInOneResponse<T>> GetResponseAsync<T>()
        {
            return await ProxyManager.GetInstance(_context).DoRequestAsync<T>(this);
        }

        public AllInOneResponse<T> GetResponse<T>()
        {
            return ProxyManager.GetInstance(_context).DoRequest<T>(this);
        }

        public Stream GetDownloadResponse()
        {
            return ProxyManager.GetInstance(_context).DoDownloadRequest(this);
        }

        private static HttpMethod GetMethod(string method)
        {
            switch (method.ToUpper())
            {
                case "GET":
                    return HttpMethod.Get;
                case "PUT":
                    return HttpMethod.Put;
                case "POST":
                    return HttpMethod.Post;
                case "DELETE":
                    return HttpMethod.Delete;
                default:
                    throw new AllInOneException("Undefined HttpMethod " + method);
            }
        }

        public void AddNonEmptyHeader(string key, string value)
        {
            if (!string.IsNullOrEmpty(value) && value != "-1")
                Headers.Add(key, value);
        }
    }
}
