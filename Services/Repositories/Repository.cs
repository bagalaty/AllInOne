using Core.Proxy.Http;
using Services.Models;
using Services.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Repositories
{
    public abstract class Repository<T>
    {
        protected readonly IAllInOneRequestManager AllInOneRequestManager;

        public Repository(IAllInOneRequestManager AllInOneRequestManager)
        {
            AllInOneRequestManager = AllInOneRequestManager;
        }

        protected BaseModel Create(string apiName, T model, List<string> parameters = null)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, true, false, parameters);
            request.Body = model;
            var response = request.GetResponse();
            return new BaseModel {
                //Message = response.message, MessageType = response.HasError ? MessageType.error : MessageType.success
            };
        }

        protected BaseModel Update(string apiName, T model, List<string> parameters = null)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, true, false, parameters);
            request.Body = model;
            var response = request.GetResponse();
            return new BaseModel
            {
                //Message = response.message, MessageType = response.HasError ? MessageType.error : MessageType.success
            };
        }

        protected bool Delete(string apiName, string id)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, true, false, new List<string> { id });
            var response = request.GetResponse<T>();
            return response.code == 10;
        }

        protected List<T> GetAll(string apiName, List<string> requestParameters = null, object body = null)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, parameters: requestParameters, isAuthorized: true);
            request.Body = body;
            var response = request.GetResponse<List<T>>();
            return response.data ?? new List<T>();
        }
        protected List<K> GetAll<K>(string apiName, List<string> requestParameters = null, object body = null)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, parameters: requestParameters, isAuthorized: true);
            request.Body = body;
            var response = request.GetResponse<List<K>>();
            return response.data ?? new List<K>();
        }

        protected T GetById(string apiName, string id)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, true, false, new List<string> { id });
            var response = request.GetResponse<T>();
            return response.data;
        }

        protected K GetById<K>(string apiName, string id = null)
        {
            var request = AllInOneRequestManager.CreateAllInOneRequest(apiName, true, false, new List<string> { id });
            var response = request.GetResponse<K>();
            return response.data;
        }

        protected void AppendLanguageHeader(AllInOneRequest request, string lang)
        {
            request.Headers.Add("lang", lang);
        }
    }
}
