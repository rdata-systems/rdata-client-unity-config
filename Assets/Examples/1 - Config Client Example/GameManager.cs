using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RData.Config.Examples.ConfigClientExample
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private RData.Authentication.JwtAuthenticationClient m_authenticationClient;

        [SerializeField] private ConfigManager m_configManager;

        [SerializeField] private InputField m_loginInput;
        [SerializeField] private InputField m_passwordInput;
        [SerializeField] private Button m_loginButton;
        [SerializeField] private Text m_statusText;

        private const string kStatusSendingAuthRequest = "Sending authentication request...";
        private const string kStatusAuthFailed = "Authentication failed";
        private const string kStatusAuthSuccessful = "Authenticated";
        private const string kStatusAuthFailedInvalidCredentials = "Invalid credentials";

        public IEnumerator Start()
        {
            if(m_authenticationClient.Authenticated)
            {
                yield return StartCoroutine(m_authenticationClient.Revoke());
            }
        }

        public void OnLoginButtonClicked()
        {
            StartCoroutine(OnLoginButtonClickedCoro());
        }

        private IEnumerator OnLoginButtonClickedCoro()
        {
            SetFormInteractable(false);
            SetStatus(kStatusSendingAuthRequest);
            yield return StartCoroutine(m_authenticationClient.Authenticate(m_loginInput.text, m_passwordInput.text));

            // Check if any errors happened during authentication
            if (m_authenticationClient.HasError && m_authenticationClient.LastError is RData.Authentication.Exceptions.InvalidCredentialsException)
            {
                SetStatus(kStatusAuthFailedInvalidCredentials);
                SetFormInteractable(true);
                yield break;
            }
            else if (m_authenticationClient.HasError)
            {
                SetError(m_authenticationClient.LastError);
                SetStatus(kStatusAuthFailed);
                SetFormInteractable(true);
                yield break;
            }

            SetStatus(kStatusAuthSuccessful);
            SetFormInteractable(true);

            m_configManager.gameObject.SetActive(true); // Start config manager
        }

        private void SetFormInteractable(bool isInteractable)
        {
            m_loginInput.interactable = isInteractable;
            m_passwordInput.interactable = isInteractable;
            m_loginButton.interactable = isInteractable;
        }

        private void SetStatus(string status)
        {
            m_statusText.text = status;
        }

        private void SetError(System.Exception exception)
        {
            m_statusText.text = string.Format("Exception: {0}", exception);
            Debug.LogException(exception);
        }
    }
}
