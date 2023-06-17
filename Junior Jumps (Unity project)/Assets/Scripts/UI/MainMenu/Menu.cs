#region

using Data.AssetsAddressables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#endregion

namespace UI.MainMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _infoButton;
        [SerializeField] private Button _exitInfoButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _infoPanel;

        [SerializeField] private GameObject _pointsMaxPanel;
        [SerializeField] private TextMeshProUGUI _pointsMaxText;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _infoButton.onClick.AddListener(OnInfoButtonClick);
            _exitInfoButton.onClick.AddListener(OnExitInfoButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
            
            #if UNITY_WEBGL
            _exitButton.gameObject.SetActive(false);
            #endif
        }

        public void SetMaxPoints(int pointsMax)
        {
            _pointsMaxPanel.SetActive(pointsMax != 0);
            _pointsMaxText.text = $"{pointsMax}";
        }

        private void OnStartButtonClick()
        {
            SceneManager.LoadScene(AssetsAddressablesContainers.GAMEPLAY_SCENE);
        }

        private void OnInfoButtonClick()
        {
            _mainPanel.SetActive(false);
            _infoPanel.SetActive(true);
        }

        private void OnExitInfoButtonClick()
        {
            _infoPanel.SetActive(false);
            _mainPanel.SetActive(true);
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}