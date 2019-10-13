using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Models;
using Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Services
{
    public class Utilities
    {
        private static object IsLogFileInUse = new object();

        public static string ClientId => Environment.GetEnvironmentVariable("clientId");

        public static string ClientSecret => Environment.GetEnvironmentVariable("clientSecret");


        private static string ListInnerExceptions(Exception ex)
        {
            string currentTrace = " \n " + ex.Message + Environment.NewLine
                                         + "  \n " + ex.StackTrace + Environment.NewLine
                                         + " \n " + ex.Source + Environment.NewLine;

            if (ex.InnerException != null)
            {
                currentTrace += ListInnerExceptions(ex.InnerException);
            }
            return currentTrace;
        }

        public static DateTime? ConvertTimeStampToDateTime(long? timeStamp)
        {
            try
            {
                if (timeStamp.HasValue)
                {
                    var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    return epoch.AddMilliseconds(timeStamp.Value);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public static long GetTimeStamp(DateTime? date)
        {
            if (!date.HasValue)
                return 0;
            return ((date.Value.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000);
        }

        public static string RemoveFirstUnderscore(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int index = name.IndexOf('_');
                if (index > -1)
                    return name.Substring(index + 1);
            }
            return name;
        }

        public static void LogError(Exception ex, HttpContext httpContext, UserDAO CurrentUser = null)
        {
            var directoryPath = "errors/";
            var filePath = directoryPath + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + "_Error.txt";

            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            if (!File.Exists(filePath))
            {
                var stream = File.Create(filePath);
                stream.Close();
            }

            var exceptionDetails = new StringBuilder();
            exceptionDetails.Append("--------------------------------------------------------------------------- " + Environment.NewLine);
            exceptionDetails.Append("Time: " + DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("00") +
                                    "- " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + Environment.NewLine);
            exceptionDetails.Append("IP: " + httpContext.Connection.RemoteIpAddress +
                                    Environment.NewLine);

            exceptionDetails.Append("User: " +
                                    (CurrentUser != null
                                        ? "" + CurrentUser.id + " - " + CurrentUser.fullName + " - " +
                                          CurrentUser.email : "Anonymous") + Environment.NewLine);

            exceptionDetails.Append(" " + ex.Message + Environment.NewLine
                                            + " " + ex.StackTrace + Environment.NewLine
                                            + " " + ex.Source + Environment.NewLine
                                            + (ex.InnerException != null ? ListInnerExceptions(ex.InnerException) : "") +
                                                "--------------------------------------------------------------------------- " + Environment.NewLine);


            lock (IsLogFileInUse)
            {
                File.AppendAllText(filePath, exceptionDetails.ToString());
            }
        }

        public static string RemoveFirstAt(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                int index = name.IndexOf('@');
                if (index > -1)
                    return name.Substring(0, index);
            }
            return name;
        }

        public static ContentType GetContentType(string type, string ext)
        {
            var result = ContentType.OTHER;
            List<string> temp;
            if (type == "AUDIO")
            {
                temp = new List<string>() { "mp3", "3gp", "wav" };
                result = temp.Contains(ext.ToLower()) ? ContentType.AUDIO : ContentType.OTHER;
            }
            else if (type == "VIDEO")
            {
                temp = new List<string>() { "mp4", "3gp", "mkv" };
                result = temp.Contains(ext.ToLower()) ? ContentType.VIDEO : ContentType.OTHER;
            }
            else if (type == "TEXT")
            {
                temp = new List<string>() { "txt", "pdf", "doc", "docx" };
                result = temp.Contains(ext.ToLower()) ? ContentType.TEXT : ContentType.OTHER;
            }
            else if (type == "IMAGE")
            {
                temp = new List<string>() { "png", "jpeg", "jpg", "bmp" };
                result = temp.Contains(ext.ToLower()) ? ContentType.IMAGE : ContentType.OTHER;
            }
            else if (type == "INTERACTIVE" || type == "ALBUM")
            {
                temp = new List<string>() { "zip" };
                result = temp.Contains(ext.ToLower()) ? ContentType.INTERACTIVE : ContentType.OTHER;
            }

            return result;
        }

        public static ContentType GetContentTypeByExt(string ext)
        {
            var result = ContentType.OTHER;
            List<string> temp = new List<string>() { "mp3", "3gp", "wav" };
            if (temp.Contains(ext.ToLower()))
            {
                result = ContentType.AUDIO;
            }
            temp = new List<string>() { "mp4", "3gp", "mkv" };
            if (temp.Contains(ext.ToLower()))
            {
                result = ContentType.VIDEO;
            }
            temp = new List<string>() { "txt", "pdf", "doc", "docx" };
            if (temp.Contains(ext.ToLower()))
            {
                result = ContentType.TEXT;
            }
            temp = new List<string>() { "png", "jpeg", "jpg", "bmp" };
            if (temp.Contains(ext.ToLower()))
            {
                result = ContentType.IMAGE;
            }
            temp = new List<string>() { "zip" };
            if (temp.Contains(ext.ToLower()))
            {
                result = ContentType.INTERACTIVE;
            }

            return result;
        }

        public static ContentType GetContentType(string type)
        {
            var result = ContentType.OTHER;
            if (type == "AUDIO")
            {
                result = ContentType.AUDIO;
            }
            else if (type == "VIDEO")
            {
                result = ContentType.VIDEO;
            }
            else if (type == "TEXT")
            {
                result = ContentType.TEXT;
            }
            else if (type == "IMAGE")
            {
                result = ContentType.IMAGE;
            }
            else if (type == "INTERACTIVE" || type == "ALBUM")
            {
                result = ContentType.INTERACTIVE;
            }

            return result;
        }

        public static API_Element_Request GetAPI_Request(string name, List<string> paramters = null)
        {
            var fileText = System.IO.File.ReadAllText("wwwroot/data/API.json");
            var result = JsonConvert.DeserializeObject<API_Request>(fileText);
            var urlElement = result.APIs.Find(api => api.Name == name);
            if (urlElement == null)
                throw new Exception("Can't find API " + name);
            if (paramters != null)
                urlElement.URL = string.Format(urlElement.URL, paramters.ToArray());
            return urlElement;//((API_Request)ser.Deserialize(myFileStream)).APIs.Find(api => api.Name == name);

            //var config = Microsoft.Extensions.Configuration.Configuration.GetSection("APISection") as APISection;
            //XElement li = config.ServerElement.OfType<Element>().FirstOrDefault(l => l.name == name);

            //if (li != null)
            //{
            //    //passing paramters to url if exist
            //    var url = li.url;
            //    if (paramters != null && paramters.Any())
            //    {
            //        url = HttpUtility.UrlDecode(url);
            //        url = string.Format(url, paramters.ToArray());
            //    }
            //    var apiRequest = new API_Request { Name = li.name, Method = li.method, URL = url };
            //    return apiRequest;
            //}
            throw new AllInOneException(name + " can't be found in web config");
        }

        public static object UploadImage(HttpRequest request)
        {
            throw new NotImplementedException();
        }

    

        /*  
         *  public static Dictionary<string, int> ConvertToPermessions(IFormCollection formCollection)
        {
            var permissions = new Dictionary<string, int>();
            var permission = "";
            ApplicationPermissionAction value;
            foreach (var key in formCollection.Keys)
            {
                if (key != "name" && key != "Organizations" && key != "id" && key != "UserTypes")
                {
                    var valueSplit = key.Split('|');
                    if (valueSplit.Length > 1)
                    {
                        permission = valueSplit[0];
                        value = (ApplicationPermissionAction)Enum.Parse(typeof(ApplicationPermissionAction), valueSplit[1]);

                        if (permissions.ContainsKey(permission))
                            permissions[permission] = permissions[permission] + (int)value;
                        else
                            permissions.Add(permission, (int)value);

                        //if ((value & ApplicationPermissionAction.View) != ApplicationPermissionAction.View) // anything but view
                        //    permissions[permission] = (int)(value | ApplicationPermissionAction.View); //add view;
                    }
                }
            }

            var updatedPermissions = new Dictionary<string, int>();
            // validate each permission
            foreach (var key in permissions.Keys)
            {
                value = (ApplicationPermissionAction)permissions[key];
                if (value != ApplicationPermissionAction.View)
                {
                    if ((value & ApplicationPermissionAction.Create) == ApplicationPermissionAction.Create)
                        value |= ApplicationPermissionAction.Update;

                    // set View to Create, Update or Delete if not set.
                    value |= ApplicationPermissionAction.View;
                    updatedPermissions.Add(key, (int)value);
                }
            }

            foreach (var item in updatedPermissions)
                permissions[item.Key] = item.Value;

            // set Space.View to all roles.
            if (permissions.ContainsKey(PermissionNames.Space))
            {
                value = (ApplicationPermissionAction)permissions[PermissionNames.Space];
                if ((value & ApplicationPermissionAction.View) != ApplicationPermissionAction.View) // anything but view
                    permissions[permission] = (int)(value | ApplicationPermissionAction.View); //add view;
            }
            else
                permissions.Add(PermissionNames.Space, (int)ApplicationPermissionAction.View); // add view

            // if space.create is checked, give permissions to contents & sharing permissions.
            value = (ApplicationPermissionAction)permissions[PermissionNames.Space];
            if ((value & ApplicationPermissionAction.Create) == ApplicationPermissionAction.Create)
            {
                value = ApplicationPermissionAction.View | ApplicationPermissionAction.Create | ApplicationPermissionAction.Update;
                if (permissions.ContainsKey(PermissionNames.SpaceContent))
                    permissions[PermissionNames.SpaceContent] = (int)value | permissions[PermissionNames.SpaceContent];
                else
                    permissions.Add(PermissionNames.SpaceContent, (int)value);

                value = ApplicationPermissionAction.View | ApplicationPermissionAction.Create | ApplicationPermissionAction.Delete;
                if (permissions.ContainsKey("allInOne.space.share"))
                    permissions["allInOne.space.share"] = (int)value;
                else
                    permissions.Add("allInOne.space.share", (int)value);
            }

            // if discussion.view is checked, give full permissions to discussion.reply
            if (permissions.ContainsKey("allInOne.space.discussion"))
            {
                var fullPermission = (int)(ApplicationPermissionAction.Create | ApplicationPermissionAction.View | ApplicationPermissionAction.Update | ApplicationPermissionAction.Delete);
                if (permissions.ContainsKey("allInOne.space.discussion.reply"))
                    permissions["allInOne.space.discussion.reply"] = fullPermission;
                else
                    permissions.Add("allInOne.space.discussion.reply", fullPermission);

                value = (ApplicationPermissionAction)permissions["allInOne.space.discussion"];
                // if create is ON and no delete permission ? add delete permission.
                if ((value & ApplicationPermissionAction.Create) == ApplicationPermissionAction.Create && (value & ApplicationPermissionAction.Delete) != ApplicationPermissionAction.Delete)
                    permissions["allInOne.space.discussion"] = fullPermission;
            }

            if (formCollection["UserTypes"] == UserTypes.SYSTEM_ADMIN.ToString() || formCollection["UserTypes"] == UserTypes.FOUNDATION_ADMIN.ToString())
            {
                // set view only to organization permission.
                if (permissions.ContainsKey(PermissionNames.Organization))
                    permissions[PermissionNames.Organization] = (int)(ApplicationPermissionAction.View | (ApplicationPermissionAction)permissions[PermissionNames.Organization]);
                else
                    permissions.Add(PermissionNames.Organization, (int)ApplicationPermissionAction.View);

                // set view only to role permission.
                if (permissions.ContainsKey(PermissionNames.Role))
                    permissions[PermissionNames.Role] = (int)(ApplicationPermissionAction.View | (ApplicationPermissionAction)permissions[PermissionNames.Role]);
                else
                    permissions.Add(PermissionNames.Role, (int)ApplicationPermissionAction.View);
            }
            if (formCollection["UserTypes"] == UserTypes.ADMIN.ToString())
            {
                // remove foundations, add foundation admin and admin (if selected).
                if (permissions.ContainsKey(PermissionNames.Foundation))
                    permissions.Remove(PermissionNames.Foundation);
                if (permissions.ContainsKey(PermissionNames.FoundationAdmin))
                    permissions.Remove(PermissionNames.FoundationAdmin);
                if (permissions.ContainsKey(PermissionNames.OrgAdmin))
                    permissions.Remove(PermissionNames.OrgAdmin);
            }

            foreach (var item in GetDefaultPermissions())
            {
                if (item.Value > -1) // -1 for unnecessary permissions.
                {
                    if (permissions.ContainsKey(item.Key))
                        permissions[item.Key] = Math.Max(item.Value, permissions[item.Key]); // get the higher permission whatever it is the default permission or the user selection
                    else
                        permissions.Add(item.Key, item.Value);
                }
            }


            try
            {
                var timeLockExceptionPermessions = permissions["allInOne.timelock.exception"];
                if (permissions.ContainsKey("allInOne.timelock"))
                {
                    var timeLockExceptionPermessionsFlag = (ApplicationPermissionAction)timeLockExceptionPermessions;
                    var timeLockPermessions = (ApplicationPermissionAction)permissions["allInOne.timelock"];
                    permissions["allInOne.timelock"] = (int)(timeLockExceptionPermessionsFlag | timeLockPermessions);
                }
                else
                    permissions["allInOne.timelock"] = timeLockExceptionPermessions;
            }
            catch { }

            return permissions;
        }
                 public static List<PermissionDAO> FilterPermissionList(List<PermissionDAO> allPermissions)
                {
                    //var allPermissions = unitOfWork.PermissionManager.GetPermissions();
                    var defaultPermissions = Utilities.GetDefaultPermissions();
                    allPermissions.RemoveAll(p => p.Application == "CL" && defaultPermissions.ContainsKey(p.Key));
                    allPermissions = allPermissions.Where(p => p.Action.Any()).ToList();
                    return allPermissions;
                }

                     */

        public static Dictionary<string, int> GetDefaultPermissions()
        {
            return new Dictionary<string, int>
            {// add auto view for content and share
                { "allInOne.activity",-1 },
               // { PermissionNames.CategoryViewer, 1 },
                //{ PermissionNames.Group, 1 },
                { "allInOne.space.assessment.report", 15 },
                { "allInOne.space.assessment.solve", 15 },
                { "allInOne.space.community", 15 }, 
                // advanced
                { "allInOne.space.content.annotation", 15 },
                // advanced
                { "allInOne.space.content.annotation.comment", 15 },
                //// advanced
                //{ "allInOne.space.discussion", -1 },
                //// advanced
                //{ "allInOne.space.discussion.replay", -1 }, // it should be "Reply" but we receive it as "Replay".
                { "allInOne.space.favorite", 15 }, // delete permission is required to "Unfavorite" space
                { "allInOne.space.join.request", -1 },
                { "allInOne.space.join.request.approve", -1 },
                { "allInOne.space.leave", 15 },
                // advanced
                { "allInOne.space.rating", 15 },
                // advanced
                { "allInOne.recommendation", -1 },
               // { PermissionNames.User, 1 },
                // advanced
                { "allInOne.user.follow", 15 }, 
                // advanced
                { "allInOne.user.group", -1 },
                // advanced
                { "allInOne.user.message", -1 },

            };
        }

        public static string GetLocalizedContentType(ContentType type)
        {
            switch (type)
            {
                case ContentType.IMAGE:
                    return Resources.Resource.Txt314;
                case ContentType.TEXT:
                    return Resources.Resource.Txt302;
                case ContentType.AUDIO:
                    return Resources.Resource.Txt303;
                case ContentType.INTERACTIVE:
                    return Resources.Resource.Txt304;
                case ContentType.VIDEO:
                    return Resources.Resource.Txt305;
                case ContentType.WORD:
                    return Resources.Resource.Txt306;
                case ContentType.PRESENTATION:
                    return Resources.Resource.Txt307;
                case ContentType.SPREAD_SHEETS:
                    return Resources.Resource.Txt308;
                case ContentType.OTHER:
                    return Resources.Resource.Txt309;
                case ContentType.URL:
                    return Resources.Resource.Txt310;
                default:
                    return Resources.Resource.Txt309;
            }
        }

        //public static string GetLocalizedAssessmentType(AssessmentType type)
        //{
        //    switch (type)
        //    {
        //        case AssessmentType.QUIZ:
        //            return Resources.Resource.Txt311;
        //        case AssessmentType.ASSIGNMENT:
        //            return Resources.Resource.Txt311;
        //        case AssessmentType.WORKSHEET:
        //            return Resources.Resource.Txt312;
        //        case AssessmentType.PRACTICE:
        //            return Resources.Resource.Txt313;
        //        case AssessmentType.CHALLENGE:
        //            return Resources.Resource.Txt315;
        //        default:
        //            return "Not Found";
        //    }
        //}
    }
}
