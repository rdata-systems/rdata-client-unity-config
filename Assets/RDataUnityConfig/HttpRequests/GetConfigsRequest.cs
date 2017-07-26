using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RData.Http;
using System;

namespace RData.Config.HttpRequests
{
    public class GetConfigsRequest : RDataHttpRequest<GetConfigsRequest.GetConfigsResponse>
    {
        public class GetConfigsResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("configs")]
            public List<RDataBaseConfig> Configs;
        }
        
        public override string Method
        {
            get { return kHttpVerbGET; }
        }

        public override string Path
        {
            get { return "/configs"; }
        }

        private string m_accessToken;

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

        public GetConfigsRequest(string accessToken)
        {
            m_accessToken = accessToken;
        }
    }

    public class GetConfigsRequest<TConfig> : RDataHttpRequest<GetConfigsRequest<TConfig>.GetConfigsResponse>
        where TConfig : RDataBaseConfig
    {
        public class GetConfigsResponse : RDataHttpResponse
        {
            [RData.LitJson.JsonAlias("configs")]
            public List<TConfig> Configs;
        }

        public override string Method
        {
            get { return kHttpVerbGET; }
        }

        public override string Path
        {
            get { return "/configs"; }
        }

        private string m_accessToken;

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

        public GetConfigsRequest(string accessToken)
        {
            m_accessToken = accessToken;
        }
    }
}