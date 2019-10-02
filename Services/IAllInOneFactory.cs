using System.Collections.Generic;
using Core.Proxy.Http;
using Services.Models;

namespace Services
{
    public interface IAllInOneRequestManager
    {
        AllInOneRequest CreateMintRequest(string apiName, bool isAuthorized = false, bool isFormUrlEncoded = false, List<string> parameters = null);
        AllInOneRequest CreateMintRequestUpload(string apiName, List<string> paramters = null);
        string OnInvalidToken();
        AllInOneRequest CreateMintDownloadRequest(string apiName, bool isAuthorized, List<string> paramters = null);

        UserDAO CurrentUser { get; }
        Core.Model.OAuth.AllInOneOAuth2Response CurrentOAuthUser { get; set; }
    }
}