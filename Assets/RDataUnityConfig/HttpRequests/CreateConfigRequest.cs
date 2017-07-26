using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RData.Http;
using System;

namespace RData.Config.HttpRequests
{
    public class CreateConfigRequest : RDataHttpRequest<CreateConfigRequest.CreateConfigResponse>
    {
        public class CreateConfigResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("config")]
            public RDataBaseConfig Config;
        }
        
        public override string Method
        {
            get { return kHttpVerbPOST; }
        }
        
        public override string Path
        {
            get { return "/configs"; }
        }

        private string m_accessToken;

        private RDataBaseConfig m_config;

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

        public override string BodyData
        {
            get
            {
                return RData.LitJson.JsonMapper.ToJson(m_config);
            }
        }

        public CreateConfigRequest(string accessToken, RDataBaseConfig config)
        {
            m_accessToken = accessToken;
            m_config = config;
        }
    }

    public class CreateConfigRequest<TConfig> : RDataHttpRequest<CreateConfigRequest<TConfig>.CreateConfigResponse>
        where TConfig : RDataBaseConfig
    {
        public class CreateConfigResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("config")]
            public TConfig Config;
        }

        public override string Method
        {
            get { return kHttpVerbPOST; }
        }

        public override string Path
        {
            get { return "/configs"; }
        }

        private string m_accessToken;

        private TConfig m_config;

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

        public override string BodyData
        {
            get
            {
                return RData.LitJson.JsonMapper.ToJson(m_config);
            }
        }

        public CreateConfigRequest(string accessToken, TConfig config)
        {
            m_accessToken = accessToken;
            m_config = config;
        }
    }
}