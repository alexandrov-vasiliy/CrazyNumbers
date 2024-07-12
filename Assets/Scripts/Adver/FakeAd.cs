using System;

namespace Adver
{
    public class FakeAd: IAd
    {
        public event Action OnRewarded;
        public void ShowAd()
        {
            OnRewarded?.Invoke();
        }
    }
}