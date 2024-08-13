using Adver;
using Analytics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkipLevelView : MonoBehaviour
{
    [Inject] private IAd _ad;
    [Inject] private LevelSwitcher _levelSwitcher;
    [Inject] private UIManager _uiManager;
    [Inject] private IAnalytics _analytics;
    [Inject] private Player _player;
    

    [SerializeField] private Text _skipLevelText;
    [SerializeField] private Text _respawnText;
    
    public void SkipLevelClick()
    {
        _ad.ShowAd();
    }

    private void RewRealise()
    {
        if (_levelSwitcher.CurrentLevel.typeLevel == LevelType.BossLevel)
        {
            SkipLevel();
        }
        else
        {
            _levelSwitcher.Respawn();
        }
        
        _analytics.WatchReward();
    }

    private void HideAllText()
    {
        _respawnText.gameObject.SetActive(false);
        _skipLevelText.gameObject.SetActive(false);
    }

    private void EnableCorrectText()
    {
        HideAllText();
        switch (_levelSwitcher.CurrentLevel.typeLevel)
        {
            case LevelType.BossLevel:
                _skipLevelText.gameObject.SetActive(true);
                break;
            case LevelType.MergeLevel:
                _respawnText.gameObject.SetActive(true);
                break;
        }
    }
    
    private void SkipLevel()
    {
        _player.ResetPosition();
        _levelSwitcher.ClearScene();
        Time.timeScale = 1;
        _levelSwitcher.NextLevel();
        _uiManager.ShowGameplay();
        
    }

    private void OnEnable()
    {
        _ad.OnRewarded += RewRealise;
        EnableCorrectText();
    }

    private void OnDisable()
    {
        _ad.OnRewarded -= RewRealise;
    }
}
