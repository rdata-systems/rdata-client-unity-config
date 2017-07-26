using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RData.Config.Examples.ConfigClientExample
{
    public class ConfigManager : MonoBehaviour
    {

        [SerializeField] private RData.Config.RDataConfigClient m_configClient;

        [SerializeField] private Transform m_configPreviewContainer;
        [SerializeField] private GameObject m_configPreviewPrefab;
        [SerializeField] private Text m_configDetailText;
        
        private IEnumerator Start()
        {
            // Step 1> Load config previews
            yield return StartCoroutine(m_configClient.LoadConfigs());

            if (m_configClient.HasError)
            {
                Debug.LogError(m_configClient.LastError);
                yield break;
            }

            // Instantiate previews - use GetConfigs()
            foreach(var config in m_configClient.GetConfigs())
            {
                var configPreview = Instantiate(m_configPreviewPrefab, m_configPreviewContainer, false);
                configPreview.GetComponentInChildren<Text>().text = config.Name;
                configPreview.GetComponentInChildren<Button>().onClick.AddListener(() => { OnConfigPreviewButtonClicked(config.Id); } );
            }
        }

        private void OnConfigPreviewButtonClicked(string configId)
        {
            StartCoroutine(LoadConfigDetails(configId));
        }
        
        private IEnumerator LoadConfigDetails(string configId)
        {
            // Load config details. Here we use generic version of GetConfig to get a config of type TConfig
            yield return StartCoroutine(m_configClient.LoadConfig<TestConfig>(configId));

            if (m_configClient.HasError)
            {
                Debug.LogError(m_configClient.LastError);
                yield break;
            }

            var config = m_configClient.GetConfig<TestConfig>(configId);

            // Set the config details text
            m_configDetailText.text = string.Format("Config Id: {0}\n" +
                "Config Name: {1}\n" +
                "Config Data.Test: {2}\n" +
                "Config Version: {3}\n",
                config.Id, config.Name, config.Data.Test, config.ConfigVersion);
        }
    }
}