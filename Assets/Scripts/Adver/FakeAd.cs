using System;

namespace Adver
{
    public abstract class FakeAd: IAd
    {
        public event Action OnRewarded;
        public void ShowAd()
        {
            OnRewarded?.Invoke();
        }

        public abstract void ShowFullScreenAd();
    }
}