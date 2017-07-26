using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RData.Config.HttpRequests;
using RData.Config.Exceptions;
using System.Linq;
using System;

namespace RData.Config
{
    [RequireComponent(typeof(RData.Http.RDataHttpClient))]
    public class RDataConfigClient : MonoBehaviour
    {
        private RData.Http.RDataHttpClient m_httpClient;
        [SerializeField] private RData.Authentication.JwtAuthenticationClient m_authenticationClient;

        public Dictionary<string, RDataBaseConfig> m_configs = new Dictionary<string, RDataBaseConfig>();

        public IEnumerable<RDataBaseConfig> Configs
        {
            get { return m_configs.Values; }
        }

        public System.Exception LastError { get; set; }

        public bool HasError
        {
            get { return LastError != null; }
        }

        private void Start()
        {
            m_httpClient = GetComponent<RData.Http.RDataHttpClient>();
        }

        public IEnumerable<RDataBaseConfig> GetConfigs()
        {
            return Configs;
        }

        public IEnumerable<TConfig> GetConfigs<TConfig>()
            where TConfig : RDataBaseConfig
        {
            List<TConfig> result = new List<TConfig>();
            foreach (var config in Configs)
            {
                try
                {
                    result.Add((TConfig)config);
                }
                catch(InvalidCastException) { }
            }
            return result;
        }

        public RDataBaseConfig GetConfig(string configId)
        {
            if(!m_configs.ContainsKey(configId))
            {
                throw new RDataConfigException("Config with id " + configId + " not found");
            }
            return m_configs[configId];
        }
        
        public TConfig GetConfig<TConfig>(string configId)
            where TConfig : RDataBaseConfig
        {
            if (!m_configs.ContainsKey(configId))
            {
                throw new RDataConfigException("Config with id " + configId + " not found");
            }

            try
            {
                return (TConfig)m_configs[configId];
            }
            catch (InvalidCastException)
            {
                throw new RDataConfigException("Config with id " + configId + " is type " + m_configs[configId].GetType() + " and can not be cast to type " + typeof(TConfig));
            }
        }

        public IEnumerator LoadConfigs()
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var getConfigsRequest = new GetConfigsRequest(m_authenticationClient.AccessToken);
            yield return StartCoroutine(m_httpClient.Send<GetConfigsRequest, GetConfigsRequest.GetConfigsResponse>(getConfigsRequest));
            
            if (getConfigsRequest.HasError)
            {
                LastError = new RDataConfigException(getConfigsRequest.Error);
                yield break;
            }
            m_configs = getConfigsRequest.Response.Configs.ToDictionary(config => config.Id, config => config);
        }

        public IEnumerator LoadConfigs<TConfig>()
            where TConfig : RDataBaseConfig
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var getConfigsRequest = new GetConfigsRequest<TConfig>(m_authenticationClient.AccessToken);
            yield return StartCoroutine(m_httpClient.Send<GetConfigsRequest<TConfig>, GetConfigsRequest<TConfig>.GetConfigsResponse>(getConfigsRequest));

            if (getConfigsRequest.HasError)
            {
                LastError = new RDataConfigException(getConfigsRequest.Error);
                yield break;
            }
            m_configs = getConfigsRequest.Response.Configs.ToDictionary(config => config.Id, config => (RDataBaseConfig) config);
        }

        public IEnumerator LoadConfig(string configId)
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var getConfigRequest = new GetConfigRequest(m_authenticationClient.AccessToken, configId);
            yield return StartCoroutine(m_httpClient.Send<GetConfigRequest, GetConfigRequest.GetConfigResponse>(getConfigRequest));

            if (getConfigRequest.HasError)
            {
                LastError = new RDataConfigException(getConfigRequest.Error);
                yield break;
            }
            var config = getConfigRequest.Response.Config;

            m_configs[config.Id] = config;
        }

        public IEnumerator LoadConfig<TConfig>(string configId)
            where TConfig : RDataBaseConfig
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var getConfigRequest = new GetConfigRequest<TConfig>(m_authenticationClient.AccessToken, configId);
            yield return StartCoroutine(m_httpClient.Send<GetConfigRequest<TConfig>, GetConfigRequest<TConfig>.GetConfigResponse>(getConfigRequest));

            if (getConfigRequest.HasError)
            {
                LastError = new RDataConfigException(getConfigRequest.Error);
                yield break;
            }
            var config = getConfigRequest.Response.Config;

            m_configs[config.Id] = config;
        }
        
        public IEnumerator CreateConfig(RDataBaseConfig config)
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var createConfigRequest = new CreateConfigRequest(m_authenticationClient.AccessToken, config);
            yield return StartCoroutine(m_httpClient.Send<CreateConfigRequest, CreateConfigRequest.CreateConfigResponse>(createConfigRequest));

            if (createConfigRequest.HasError)
            {
                LastError = new RDataConfigException(createConfigRequest.Error);
                yield break;
            }

            m_configs[config.Id] = createConfigRequest.Response.Config;
        }

        public IEnumerator CreateConfig<TConfig>(TConfig config)
            where TConfig : RDataBaseConfig
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var createConfigRequest = new CreateConfigRequest<TConfig>(m_authenticationClient.AccessToken, config);
            yield return StartCoroutine(m_httpClient.Send<CreateConfigRequest<TConfig>, CreateConfigRequest<TConfig>.CreateConfigResponse>(createConfigRequest));

            if (createConfigRequest.HasError)
            {
                LastError = new RDataConfigException(createConfigRequest.Error);
                yield break;
            }

            m_configs[config.Id] = (TConfig)createConfigRequest.Response.Config;
        }

        public IEnumerator UpdateConfig(RDataBaseConfig config)
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var updateConfigRequest = new UpdateConfigRequest(m_authenticationClient.AccessToken, config);
            yield return StartCoroutine(m_httpClient.Send<UpdateConfigRequest, UpdateConfigRequest.UpdateConfigResponse>(updateConfigRequest));

            if (updateConfigRequest.HasError)
            {
                LastError = new RDataConfigException(updateConfigRequest.Error);
                yield break;
            }

            m_configs[config.Id] = updateConfigRequest.Response.Config;
        }

        public IEnumerator UpdateConfig<TConfig>(TConfig config)
            where TConfig : RDataBaseConfig
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var updateConfigRequest = new UpdateConfigRequest<TConfig>(m_authenticationClient.AccessToken, config);
            yield return StartCoroutine(m_httpClient.Send<UpdateConfigRequest<TConfig>, UpdateConfigRequest<TConfig>.UpdateConfigResponse>(updateConfigRequest));

            if (updateConfigRequest.HasError)
            {
                LastError = new RDataConfigException(updateConfigRequest.Error);
                yield break;
            }

            m_configs[config.Id] = (TConfig)updateConfigRequest.Response.Config;
        }

        public IEnumerator DeleteConfig(RDataBaseConfig config)
        {
            yield return StartCoroutine(m_authenticationClient.EnsureValidAccessToken());

            var deleteConfigRequest = new DeleteConfigRequest(m_authenticationClient.AccessToken, config.Id);
            yield return StartCoroutine(m_httpClient.Send<DeleteConfigRequest, DeleteConfigRequest.DeleteConfigResponse>(deleteConfigRequest));

            if (deleteConfigRequest.HasError)
            {
                LastError = new RDataConfigException(deleteConfigRequest.Error);
                yield break;
            }

            m_configs.Remove(config.Id);
        }

        public IEnumerator DeleteConfig<TConfig>(TConfig config)
            where TConfig : RDataBaseConfig
        {
            yield return StartCoroutine(DeleteConfig((RDataBaseConfig)config));
        }
    }
}