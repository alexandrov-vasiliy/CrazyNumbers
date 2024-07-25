using System;
using System.Collections;
using System.Collections.Generic;
using Analytics;
using Level;
using Levels;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levels;

    [SerializeField] private int _currentLevelIndex = 0;
    public int CurrentLevelIndex => _currentLevelIndex;
    private int _previousLevelIndex;

    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private GameObject _borders;
    [SerializeField] private GameObject _killZone;

    public Action<int> OnCurrentLevelChange;

    private AudioManager _audioManager;
    private UIManager _uIManager;
    private ObstacleSpawner _obstacleSpawner;
    private PlayerForce _playerForce;
    private Player _player;
    private ILevelSaver _levelSaver;
    private PlayerEvents _playerEvents;
    private IAnalytics _analytics;
    
    public int _obstacleCount;
    public int _obstacleReceived = 0;

    [Inject]
    public void Construct(
        AudioManager audioManager,
        PlayerForce playerForce,
        UIManager uiManager,
        ObstacleSpawner obstacleSpawner,
        Player player,
        ILevelSaver levelSaver,
        PlayerEvents playerEvents,
        IAnalytics analytics
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

    private void HandleApplyObstacle(ObstacleType type)
    {
        if (type == ObstacleType.Obstacle)
        {
            _obstacleReceived++;
        }

        if (_obstacleReceived == _obstacleCount)
        {
            StartShowLevelComplete();
            ClearScene(true);
            _obstacleReceived = 0;
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
        if (_uIManager.gameState == GameState.PAUSED)
        {
            Time.timeScale = 1f;
        }

        _obstacleReceived = 0;

        ClearScene();
        _obstacleSpawner.StopSpawn();
        _uIManager.ShowGameplay();
        PlayLevel();
        _playerForce.ResetCurrentForce();
    }

    private void Start()
    {
        _currentLevelIndex = _levelSaver.GetSavedLevel();
        OnCurrentLevelChange?.Invoke(_currentLevelIndex);
        Debug.Log($"level sfitcher get level {_currentLevelIndex}");
    }


    public void ClearScene(bool partialExecution = false)
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var t in array)
        {
            t.SetActive(false);
        }

        if (partialExecution)
        {
            return;
        }

        _player.gameObject.transform.position = new Vector2(0f, -2.5f);

        _player.gameObject.gameObject.SetActive(true);
    }

    public void StartShowLevelComplete()
    {
        StartCoroutine(ShowLevelComplete());
    }

    private IEnumerator ShowLevelComplete()
    {
        _levelCompletePanel.SetActive(true);
        yield return new WaitForSeconds(1f);
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
        _analytics.SendGoal("LEVEL_COMPLETE ", _currentLevelIndex + 1);
    }

    public void PlayLevel()
    {
        _obstacleSpawner.levelConfig = _levels[_currentLevelIndex];
        ChangeLevelType(_obstacleSpawner.levelConfig.typeLevel);
        _obstacleCount = _obstacleSpawner.levelConfig.ObstacleCount;
        _obstacleSpawner.StartSpawn();
    }

    public void GameOver()
    {
        if (_uIManager.gameState == GameState.PLAYING)
        {
            _player.gameObject.SetActive(false);
            ClearScene();
            _obstacleSpawner.StopSpawn();
            _audioManager.PlayEffects(_audioManager.gameOver);
            _uIManager.ShowGameOver();
            _playerForce.UpdateScoreGameover();
        }
    }
}