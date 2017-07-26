using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RData.Http;
using System;

namespace RData.Config.HttpRequests
{
    public class UpdateConfigRequest : RDataHttpRequest<UpdateConfigRequest.UpdateConfigResponse>
    {
        public class UpdateConfigResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("config")]
            public RDataBaseConfig Config;
        }
        
        public override string Method
        {
            get { return kHttpVerbPUT; }
        }
        
        public override string Path
        {
            get { return "/configs/" + m_config.Id; }
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

        public UpdateConfigRequest(string accessToken, RDataBaseConfig config)
        {
            m_accessToken = accessToken;
            m_config = config;
        }
    }

    public class UpdateConfigRequest<TConfig> : RDataHttpRequest<UpdateConfigRequest<TConfig>.UpdateConfigResponse>
        where TConfig : RDataBaseConfig
    {
        public class UpdateConfigResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("config")]
            public TConfig Config;
        }

        public override string Method
        {
            get { return kHttpVerbPUT; }
        }

        public override string Path
        {
            get { return "/configs/" + m_config.Id; }
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

        public UpdateConfigRequest(string accessToken, TConfig config)
        {
            m_accessToken = accessToken;
            m_config = config;
        }
    }
}