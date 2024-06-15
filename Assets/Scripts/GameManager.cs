using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameManager : MonoBehaviour
{
	

	[FormerlySerializedAs("scoreManager")] public PlayerForce playerForce;

	[Space(5f)]
	public Player player;
	
	
	[Header("Game settings")]
	[Space(5f)]
	public Material trailMaterial;

	[Space(5f)]
	public Color[] colorTable;


	private GameObject tempObstacle;

	private Vector2 tempPos;

	private Vector3 screenSize;

	private Color color;

	private AudioManager _audioManager;
	private PlayerForce _playerForce;
	private UIManager _uIManager;
	[Inject]
	public void Construct(AudioManager audioManager, PlayerForce playerForce, UIManager uiManager)
	{
		_audioManager = audioManager;
		_playerForce = playerForce;
		_uIManager = uiManager;
	}
	

	public static GameManager Instance
	{
		get;
		set;
	}

	private void Awake()
	{
		DontDestroyOnLoad(this);
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		color = colorTable[Random.Range(0, colorTable.Length)];	
		player.SetColor(color);
		trailMaterial.color = color;
	}

	
	public void RestartGame()
	{
		if (_uIManager.gameState == GameState.PAUSED)
		{
			Time.timeScale = 1f;
		}
		_uIManager.ShowGameplay();
		ClearScene();
		playerForce.ResetCurrentScore();
	}

	public void ClearScene()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("Obstacle");
		foreach (var t in array)
		{
			t.SetActive(false);
		}
		player.transform.position = new Vector2(0f, -2.5f);
		color = colorTable[Random.Range(0, colorTable.Length)];
		player.SetColor(color);
		trailMaterial.color = color;
		player.gameObject.SetActive(true);
	}

	public void GameOver()
	{
		if (_uIManager.gameState == GameState.PLAYING)
		{
			player.gameObject.SetActive(false);
			
			StopAllCoroutines();
			_audioManager.PlayEffects(_audioManager.gameOver);
			_uIManager.ShowGameOver();
			playerForce.UpdateScoreGameover();
		}
	}
}
