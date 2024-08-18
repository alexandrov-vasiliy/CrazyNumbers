using System;
using UnityEngine;
using YG;
using Zenject;

namespace Adver
{
    public class AdvertEvents: IInitializable, IDisposable
    {
        private readonly AudioManager _audioManager;
        private UIManager _uiManager;

        public AdvertEvents(AudioManager audioManager, UIManager uiManager)
        {
            _audioManager = audioManager;
            _uiManager = uiManager;
        }
        
        public void OnAdClose()
        {
            Debug.Log("FullScreen Ad Close ");
            Time.timeScale = 1;
            AdNotificationYG.Instance?.HideAdNotification();
        }

        public void Initialize()
        {
            YandexGame.CloseFullAdEvent += OnAdClose;
            YandexGame.onVisibilityWindowGame += OnVisibilityWindowGame;

        }

        public void Dispose()
        {
            YandexGame.CloseFullAdEvent -= OnAdClose;
            YandexGame.onVisibilityWindowGame -= OnVisibilityWindowGame;
        }
        
        void OnVisibilityWindowGame(bool visible)
        {
            if (visible) return;
            
            _audioManager.MuteEfx();
            _audioManager.MuteMusic();
            if (_uiManager.gameState == GameState.PLAYING)
            {
                _uiManager.ShowPauseMenu();
            }
        }
    }
}