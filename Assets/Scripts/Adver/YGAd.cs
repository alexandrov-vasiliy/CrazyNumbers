using System;
using UnityEngine;
using YG;
using Zenject;

namespace Adver
{
   
    public class YGAd: IAd, IInitializable, IDisposable
    {
        public event Action OnRewarded;

        
        
        
        public void ShowAd()
        {
            YandexGame.RewVideoShow(1);
        }

        public void ShowFullScreenAd()
        {
            YandexGame.FullscreenShow();
        }

        public void Reward(int id)
        {
            Debug.Log($"Reward id: {id}");
            if (id == 1)
            {
                OnRewarded?.Invoke();
            }
            
        }

        public void Initialize()
        {
            YandexGame.RewardVideoEvent += Reward;
        }

        public void Dispose()
        {
            YandexGame.RewardVideoEvent -= Reward;
        }
    }
}