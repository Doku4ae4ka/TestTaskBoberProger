using System;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.SignalService;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class WinScreen : MonoBehaviour
    {
        [SerializeField] private GameObject winScreen;
        private ISignalSubscriber _signalSubscriber;
        private void Awake()
        {
            _signalSubscriber = AllServices.Container.Single<ISignalSubscriber>();
            winScreen.SetActive(false);
        }

        private void Start()
        {
            _signalSubscriber.Subscribe<Signals.OnDoorOpened>(ShowWinScreen);
        }
        
        public void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void ShowWinScreen(Signals.OnDoorOpened data)
        {
            winScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }
}