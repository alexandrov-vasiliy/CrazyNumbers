using System;
using YandexMobileAds;
using YandexMobileAds.Base;
using Zenject;

namespace Adver
{
    public class AndroidYandexAd : IAd, IInitializable, IDisposable
    {
        private const string rewardedId = "R-M-9743258-1";
        public event Action OnRewarded;

        private RewardedAdLoader rewardedAdLoader;
        private RewardedAd rewardedAd;

        private void SetupLoader()
        {
            rewardedAdLoader = new RewardedAdLoader();
            rewardedAdLoader.OnAdLoaded += HandleAdLoaded;
        }

        public void Initialize()
        {
            SetupLoader();
            RequestRewarded();
        }

        public void ShowAd()
        {
            rewardedAd?.Show();
        }

        private void RequestRewarded()
        {
            AdRequestConfiguration adRequestConfiguration = new AdRequestConfiguration.Builder(rewardedId).Build();
            rewardedAdLoader.LoadAd(adRequestConfiguration);
            
        }
        
        public void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
        {
            // Rewarded ad was loaded successfully. Now you can handle it.
            rewardedAd = args.RewardedAd;
            
            rewardedAd.OnRewarded += HandleRewarded;
        }
        

        public void HandleRewarded(object sender, Reward args)
        {
            OnRewarded?.Invoke();
        }

        public void Dispose()
        {
            if (rewardedAd == null) return;
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }
}