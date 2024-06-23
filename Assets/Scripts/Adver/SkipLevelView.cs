using System;
using Adver;
using UnityEngine;
using Zenject;

public class SkipLevelView : MonoBehaviour
{
    [Inject] private IAd _ad;
    [Inject] private LevelSwitcher _levelSwitcher;
    [Inject] private UIManager _uiManager;

    public void SkipLevelClick()
    {
        _ad.ShowAd();
    }

    private void SkipLevel()
    {
        _uiManager.ShowGameplay();
        _levelSwitcher.NextLevel();
        
    }

    private void OnEnable()
    {
        _ad.OnRewarded += SkipLevel;
    }

    private void OnDisable()
    {
        _ad.OnRewarded -= SkipLevel;
    }
}
