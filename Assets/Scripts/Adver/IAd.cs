using System;

namespace Adver
{
    public interface IAd
    {

		public event Action OnRewarded;

        public void ShowAd();


    }
}