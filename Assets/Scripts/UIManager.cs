using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
	[Header("GUI Components")]
	public GameObject mainMenuGui;

	public GameObject pauseGui;

	public GameObject gameplayGui;

	public GameObject gameOverGui;

	public GameState gameState;

	private bool clicked;

	private AudioManager _audioManager;
	private PlayerForce _playerForce;

	[Inject]
	public void Construct(AudioManager audioManager, PlayerForce playerForce)
	{
		_audioManager = audioManager;
		_playerForce = playerForce;
	}

	private void Start()
	{
		mainMenuGui.SetActive(value: true);
		pauseGui.SetActive(value: false);
		gameplayGui.SetActive(value: false);
		gameOverGui.SetActive(value: false);
		gameState = GameState.MENU;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && gameState == GameState.MENU && !clicked)
		{
			if (!IsButton())
			{
				_audioManager.PlayEffects(_audioManager.buttonClick);
				ShowGameplay();
			}
		}
		else if (Input.GetMouseButtonUp(0) && clicked && gameState == GameState.MENU)
		{
			clicked = false;
		}
	}

	public void ShowMainMenu()
	{
		_playerForce.ResetCurrentScore();
		clicked = true;
		mainMenuGui.SetActive(value: true);
		pauseGui.SetActive(value: false);
		gameplayGui.SetActive(value: false);
		gameOverGui.SetActive(value: false);
		if (gameState == GameState.PAUSED)
		{
			Time.timeScale = 1f;
		}
		gameState = GameState.MENU;
		_audioManager.PlayEffects(_audioManager.buttonClick);
		GameManager.Instance.ClearScene();
	}

	public void ShowPauseMenu()
	{
		if (gameState != GameState.PAUSED)
		{
			pauseGui.SetActive(value: true);
			Time.timeScale = 0f;
			gameState = GameState.PAUSED;
			_audioManager.PlayEffects(_audioManager.buttonClick);
		}
	}

	public void HidePauseMenu()
	{
		pauseGui.SetActive(value: false);
		Time.timeScale = 1f;
		gameState = GameState.PLAYING;
		_audioManager.PlayEffects(_audioManager.buttonClick);
	}

	public void ShowGameplay()
	{
		mainMenuGui.SetActive(value: false);
		pauseGui.SetActive(value: false);
		gameplayGui.SetActive(value: true);
		gameOverGui.SetActive(value: false);
		gameState = GameState.PLAYING;
		_audioManager.PlayEffects(_audioManager.buttonClick);
		_audioManager.PlayMusic(_audioManager.gameMusic);
	}

	public void ShowGameOver()
	{
		mainMenuGui.SetActive(value: false);
		pauseGui.SetActive(value: false);
		gameplayGui.SetActive(value: false);
		gameOverGui.SetActive(value: true);
		gameState = GameState.GAMEOVER;
		_audioManager.PlayMusic(_audioManager.menuMusic);
	}

	public bool IsButton()
	{
		bool flag = false;
		PointerEventData eventData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, list);
		foreach (RaycastResult item in list)
		{
			flag |= (item.gameObject.GetComponent<Button>() != null);
		}
		return flag;
	}
}
