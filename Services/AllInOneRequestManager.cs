using System;
using System.Collections.Generic;
using System.Text;
using Core.Model.OAuth;
using Core.Proxy.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.Models;

namespace Services
{
    public class AllInOneRequestManager : IAllInOneRequestManager
    {
        private readonly IHttpContextAccessor _context;
        private IHostingEnvironment _hostingEnvironment;

        public AllInOneRequestManager(IHttpContextAccessor context, IHostingEnvironment env)
        {
            _context = context;
            _hostingEnvironment = env;

            //  _mintRequestManager = mintRequestManager;
        }
        public UserDAO CurrentUser => throw new NotImplementedException();

        public AllInOneOAuth2Response CurrentOAuthUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public AllInOneRequest CreateAllInOneDownloadRequest(string apiName, bool isAuthorized, List<string> paramters = null)
        {
            throw new NotImplementedException();
        }

        public AllInOneRequest CreateAllInOneRequest(string apiName, bool isAuthorized = false, bool isFormUrlEncoded = false, List<string> parameters = null)
        {
            throw new NotImplementedException();
        }

        public AllInOneRequest CreateAllInOneRequestUpload(string apiName, List<string> paramters = null)
        {
            throw new NotImplementedException();
        }

        public string OnInvalidToken()
        {
            throw new NotImplementedException();
        }
    }
}
