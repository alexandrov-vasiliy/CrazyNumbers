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

    private void RewRealise()
    {
        if (_levelSwitcher._levels[_levelSwitcher.CurrentLevelIndex].typeLevel == LevelType.BossLevel)
        {
            SkipLevel();
        }
        else
        {
            _levelSwitcher.Respawn();
        }
    }

    private void SkipLevel()
    {
        _levelSwitcher.ClearScene();
        Time.timeScale = 1;
        _levelSwitcher.NextLevel();
        _uiManager.ShowGameplay();
        
    }

    private void OnEnable()
    {
        _ad.OnRewarded += RewRealise;
    }

    private void OnDisable()
    {
        _ad.OnRewarded -= RewRealise;
    }
}
