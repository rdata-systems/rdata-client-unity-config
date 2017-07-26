using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RData.Http;
using System;

namespace RData.Config.HttpRequests
{
    public class DeleteConfigRequest : RDataHttpRequest<DeleteConfigRequest.DeleteConfigResponse>
    {
        public class DeleteConfigResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("config")]
            public RDataBaseConfig Config;
        }
        
        public override string Method
        {
            get { return kHttpVerbDELETE; }
        }

        public override string Path
        {
            get { return "/configs/" + m_configId; }
        }

        private string m_accessToken;

        private string m_configId;

        public override Dictionary<string, string> Headers
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "Authorization", "Bearer " + m_accessToken }
                };
            }
        }

        public DeleteConfigRequest(string accessToken, string configId)
        {
            m_accessToken = accessToken;
            m_configId = configId;
        }
    }
    public class DeleteConfigRequest<TConfig> : RDataHttpRequest<DeleteConfigRequest<TConfig>.DeleteConfigResponse>
        where TConfig : RDataBaseConfig
    {
        public class DeleteConfigResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("config")]
            public TConfig Config;
        }

        public override string Method
        {
            get { return kHttpVerbDELETE; }
        }

        public override string Path
        {
            get { return "/configs/" + m_configId; }
        }

        private string m_accessToken;

        private string m_configId;

        public override Dictionary<string, string> Headers
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { "Authorization", "Bearer " + m_accessToken }
                };
            }
        }

        public DeleteConfigRequest(string accessToken, string configId)
        {
            m_accessToken = accessToken;
            m_configId = configId;
        }
    }
}