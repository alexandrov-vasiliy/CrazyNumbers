using System;
using Adver;
using Zenject;

public class FullScreenAdShower : IInitializable, IDisposable
{
    
    

    private int _dieCount = 0;
    private int _dieStep = 2;
    
    private readonly IAd _ad;
    private readonly PlayerEvents _playerEvents;
    
    public FullScreenAdShower(PlayerEvents playerEvents, IAd ad, int dieStep)
    {
        _dieStep = dieStep;
        _playerEvents = playerEvents;
        _ad = ad;
    }
    
    
    public void Initialize()
    {
        _playerEvents.OnPlayerDead += HandlePlayerDead;
    }

    public void Dispose()
    {
        _playerEvents.OnPlayerDead -= HandlePlayerDead;
    }

    private void HandlePlayerDead()
    {
        _dieCount++;
    }

    public void TryShowFullscreenAd()
    {
        if (_dieCount >= _dieStep)
        {
            _ad.ShowFullScreenAd();
            _dieCount = 0;
        }

    }
    
}

