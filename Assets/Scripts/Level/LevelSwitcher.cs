using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Level;
using Levels;
using UnityEngine;
using Zenject;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levels;

    [SerializeField] private int _currentLevelIndex = 0;
    public int CurrentLevelIndex => _currentLevelIndex;

    [SerializeField] private GameObject _levelCompletePanel;

    
    
    public Action<int> OnCurrentLevelChange;
    
    private AudioManager _audioManager;
    private UIManager _uIManager;
    private ObstacleSpawner _obstacleSpawner;
    private PlayerForce _playerForce;
    private Player _player;
    private ILevelSaver _levelSaver;
    private PlayerEvents _playerEvents;
    [Inject]
    public void Construct(
        AudioManager audioManager,
        PlayerForce playerForce,
        UIManager uiManager,
        ObstacleSpawner obstacleSpawner, 
        Player player,
        ILevelSaver levelSaver,
        PlayerEvents playerEvents
        )
    {
        _audioManager = audioManager;
        _uIManager = uiManager;
        _obstacleSpawner = obstacleSpawner;
        _playerForce = playerForce;
        _player = player;
        _levelSaver = levelSaver;
        _playerEvents = playerEvents;
    }
    
    private void OnEnable()
    {
        _playerEvents.OnPlayerDead += GameOver;
        _playerEvents.OnLevelComplete += StartShowLevelComplete;
    }

    private void OnDisable()
    {
        _playerEvents.OnPlayerDead -= GameOver;
        _playerEvents.OnLevelComplete -= NextLevel;
    }

    public void RestartGame()
    {
        if (_uIManager.gameState == GameState.PAUSED)
        {
            Time.timeScale = 1f;
        }
        
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


    public void ClearScene()
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var t in array)
        {
            t.SetActive(false);
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
        if (_levels.Count-1 > _currentLevelIndex)
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
    }

    public void PlayLevel()
    {
        _obstacleSpawner.levelConfig = _levels[_currentLevelIndex];

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