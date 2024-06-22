using System;
using System.Collections.Generic;
using Level;
using Levels;
using UnityEngine;
using Zenject;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levels;

    private int _currentLevelIndex = 0;

    private AudioManager _audioManager;
    private UIManager _uIManager;
    private ObstacleSpawner _obstacleSpawner;
    private PlayerForce _playerForce;
    private Player _player;
   // private ILevelSaver _levelSaver;
    private PlayerEvents _playerEvents;
    [Inject]
    public void Construct(
        AudioManager audioManager,
        PlayerForce playerForce,
        UIManager uiManager,
        ObstacleSpawner obstacleSpawner, 
        Player player,
        //ILevelSaver levelSaver,
        PlayerEvents playerEvents
        )
    {
        _audioManager = audioManager;
        _uIManager = uiManager;
        _obstacleSpawner = obstacleSpawner;
        _playerForce = playerForce;
        _player = player;
      //  _levelSaver = levelSaver;
        _playerEvents = playerEvents;
    }

    private void Start()
    {
        PlayLevel();
    }

    private void OnEnable()
    {
        _playerEvents.OnPlayerDead += GameOver;
    }

    private void OnDisable()
    {
        _playerEvents.OnPlayerDead -= GameOver;
    }

    public void RestartGame()
    {
        if (_uIManager.gameState == GameState.PAUSED)
        {
            Time.timeScale = 1f;
        }

        _uIManager.ShowGameplay();
        ClearScene();
        _playerForce.ResetCurrentScore();
        _obstacleSpawner.StartSpawn();
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


    public void NextLevel()
    {
        _currentLevelIndex++;
   //     _levelSaver.SaveLevel(_currentLevelIndex);
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
            StopAllCoroutines();
            _audioManager.PlayEffects(_audioManager.gameOver);
            _uIManager.ShowGameOver();
            _playerForce.UpdateScoreGameover();
        }
    }
}