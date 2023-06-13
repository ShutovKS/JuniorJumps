#region

using Data.AssetsAddressables;
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
        [SerializeField] private Button _exitButton;

        [SerializeField] private GameObject _mainPanel;

        private void Start()
        {
            _startButton.onClick.AddListener(OnStartButtonClick);
            _infoButton.onClick.AddListener(OnInfoButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnStartButtonClick()
        {
            SceneManager.LoadScene(AssetsAddressablesContainers.GAMEPLAY_SCENE);
        }

        private void OnInfoButtonClick()
        {
            // _mainPanel.SetActive(false);
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}