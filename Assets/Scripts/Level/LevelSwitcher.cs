using System;
using System.Collections;
using System.Collections.Generic;
using Analytics;
using Level;
using Levels;
using Localization;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levels;


    [SerializeField] private int _overrideLevelIndex = 0;
    [SerializeField] private Text _tutorialText;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _borders;
    [SerializeField] private GameObject _killZone;
    [SerializeField] private int _delayStartLevel;

    [Header("CompleteLevel")] [SerializeField, Range(1f, 5f)]
    private float _completeLevelDelay = 2f;

    [SerializeField] private ParticleSystem[] _completeLevelParticles;


    [ShowNonSerializedField] private int _obstacleReceived = 0;

    private int _currentLevelIndex = 0;

    private AudioManager _audioManager;
    private UIManager _uIManager;
    private ObstacleSpawner _obstacleSpawner;
    private PlayerForce _playerForce;
    private Player _player;
    private ILevelSaver _levelSaver;
    private PlayerEvents _playerEvents;
    private IAnalytics _analytics;
    private ILocalization _localization;
    private FullScreenAdShower _fullScreenAdShower;

    public Action<int> OnCurrentLevelChange;
    public int CurrentLevelIndex => _currentLevelIndex;
    public LevelConfig CurrentLevel => _levels[_currentLevelIndex];

    [Inject]
    public void Construct(
        AudioManager audioManager,
        PlayerForce playerForce,
        UIManager uiManager,
        ObstacleSpawner obstacleSpawner,
        Player player,
        ILevelSaver levelSaver,
        PlayerEvents playerEvents,
        IAnalytics analytics,
        FullScreenAdShower fullScreenAdShower,
        ILocalization localization
    )
    {
        _audioManager = audioManager;
        _uIManager = uiManager;
        _obstacleSpawner = obstacleSpawner;
        _playerForce = playerForce;
        _player = player;
        _levelSaver = levelSaver;
        _playerEvents = playerEvents;
        _analytics = analytics;
        _localization = localization;
        _fullScreenAdShower = fullScreenAdShower;
    }

    private void OnEnable()
    {
        _playerEvents.OnPlayerDead += GameOver;
        _playerEvents.OnLevelComplete += StartShowLevelComplete;

        if (_obstacleSpawner.levelConfig.typeLevel == LevelType.BossLevel)
        {
            return;
        }

        _playerEvents.OnPlayerApplyObstacle += HandleApplyObstacle;
    }

    private void OnDisable()
    {
        _playerEvents.OnPlayerDead -= GameOver;
        _playerEvents.OnLevelComplete -= NextLevel;

        if (_obstacleSpawner.levelConfig.typeLevel == LevelType.BossLevel)
        {
            return;
        }

        _playerEvents.OnPlayerApplyObstacle -= HandleApplyObstacle;
    }

    private void Start()
    {
        _currentLevelIndex = _levelSaver.GetSavedLevel();
        OnCurrentLevelChange?.Invoke(_currentLevelIndex);

#if UNITY_EDITOR
        _currentLevelIndex = _overrideLevelIndex;
#endif
    }


    private void HandleApplyObstacle(ObstacleType type)
    {
        if (type == ObstacleType.Obstacle)
        {
            _obstacleReceived++;
        }

        if (_obstacleReceived == CurrentLevel.ObstacleCount)
        {
            StartShowLevelComplete();
            ClearScene();
        }
    }

    private void ChangeLevelType(LevelType type)
    {
        if (type == LevelType.BossLevel)
        {
            _borders.SetActive(false);
            _killZone.SetActive(true);
        }
        else if (type == LevelType.MergeLevel)
        {
            _borders.SetActive(true);
            _killZone.SetActive(false);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        ClearScene();

        _player.ResetPosition();
        _obstacleSpawner.StopSpawn();
        _uIManager.ShowGameplay();

        PlayLevel();

        _playerForce.ResetCurrentForce();

        _fullScreenAdShower.TryShowFullscreenAd();
    }


    public void Respawn()
    {
        _uIManager.ShowGameplay();
        _playerEvents.CanDead = false;
        Time.timeScale = 1;
        StartCoroutine(Immortality());
    }

    private IEnumerator Immortality()
    {
        yield return new WaitForSeconds(2);
        _playerEvents.CanDead = true;
        yield break;
    }

    public void ClearScene()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var t in array)
        {
            t.SetActive(false);
        }

        _obstacleReceived = 0;
    }


    public void StartShowLevelComplete()
    {
        StartCoroutine(ShowLevelComplete());
    }

    private IEnumerator ShowLevelComplete()
    {
        _levelCompletePanel.SetActive(true);
        _tutorialText.text = "";
        foreach (var particle in _completeLevelParticles)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }

        yield return new WaitForSeconds(_completeLevelDelay);
        _levelCompletePanel.SetActive(false);
        NextLevel();
    }


    public void NextLevel()
    {
        if (_levels.Count - 1 > _currentLevelIndex)
        {
            _currentLevelIndex++;
        }
        else
        {
            _currentLevelIndex = 0;
        }

        OnCurrentLevelChange?.Invoke(_currentLevelIndex);
        _levelSaver.SaveLevel(_currentLevelIndex);
        PlayLevel();
        _playerForce.ResetCurrentForce();
        _analytics.CompleteLevel(_currentLevelIndex);
    }

    public void PlayLevel()
    {
        StartCoroutine(DelayStartLevel());
        SetTutorialText();
    }

    private IEnumerator DelayStartLevel()
    {
        yield return new WaitForSeconds(_delayStartLevel);
        ChangeLevelType(CurrentLevel.typeLevel);
        _obstacleSpawner.levelConfig = CurrentLevel;
        _obstacleSpawner.StartSpawn();

    }

    private void SetTutorialText()
    {
        if (_localization.GetLang() == "ru")
        {
            _tutorialText.text = CurrentLevel.Tutorial.RuText;
        }

        if (_localization.GetLang() == "en")
        {
            _tutorialText.text = CurrentLevel.Tutorial.EnText;
        }
    }

    public void GameOver()
    {
        if (_uIManager.gameState == GameState.PLAYING)
        {
            Time.timeScale = 0;
            _audioManager.PlayEffects(_audioManager.gameOver);
            _uIManager.ShowGameOver();
        }
    }
}