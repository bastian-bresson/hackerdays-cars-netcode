using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(StartGameVroomVroom);
        }

        private void StartGameVroomVroom()
        {
            SceneManager.LoadSceneAsync("TheBestNetworkingGame", LoadSceneMode.Single);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
        }
    }
}
