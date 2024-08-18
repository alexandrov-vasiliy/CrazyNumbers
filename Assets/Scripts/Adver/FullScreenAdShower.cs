namespace Adver
{
    public class FullScreenAdShower
    {
        private readonly IAd _ad;
        public FullScreenAdShower(IAd ad)
        {
            _ad = ad;
        }
        
        public void TryShowFullscreenAd()
        {
            _ad.ShowFullScreenAd();
        }
    }
}

