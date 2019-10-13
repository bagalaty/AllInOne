using System.Collections.Generic;
using Core.Proxy.Http;
using Services.Models;

namespace Services
{
    public interface IAllInOneRequestManager
    {
        AllInOneRequest CreateAllInOneRequest(string apiName, bool isAuthorized = false, bool isFormUrlEncoded = false, List<string> parameters = null);
        AllInOneRequest CreateAllInOneRequestUpload(string apiName, List<string> paramters = null);
        string OnInvalidToken();
        AllInOneRequest CreateAllInOneDownloadRequest(string apiName, bool isAuthorized, List<string> paramters = null);

        UserDAO CurrentUser { get; }
        Core.Model.OAuth.AllInOneOAuth2Response CurrentOAuthUser { get; set; }
    }
}