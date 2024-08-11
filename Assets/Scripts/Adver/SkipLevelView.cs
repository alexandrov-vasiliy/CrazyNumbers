using Adver;
using Analytics;
using UnityEngine;
using Zenject;

public class SkipLevelView : MonoBehaviour
{
    [Inject] private IAd _ad;
    [Inject] private LevelSwitcher _levelSwitcher;
    [Inject] private UIManager _uiManager;
    [Inject] private IAnalytics _analytics;
    public void SkipLevelClick()
    {
        _ad.ShowAd();
    }

    private void SkipLevel()
    {
        _uiManager.ShowGameplay();
        _levelSwitcher.NextLevel();
        _analytics.WatchReward();
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
