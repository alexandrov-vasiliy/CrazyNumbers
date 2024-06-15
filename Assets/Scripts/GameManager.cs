using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameManager : MonoBehaviour
{
	public UIManager uIManager;

	[FormerlySerializedAs("scoreManager")] public PlayerForce playerForce;

	[Space(5f)]
	public Player player;
	
	
	[Header("Game settings")]
	[Space(5f)]
	public Material trailMaterial;

	[Space(5f)]
	public Color[] colorTable;



	[Space(5f)]
	public float minTimeBetweenObstacles = 0.5f;

	public float startTimeBetweenObstacles = 1f;
	

	private float currentTimeBetweenObstacles;

	private bool spawning;

	private GameObject tempObstacle;

	private Vector2 tempPos;

	private Vector3 screenSize;

	private Color color;

	private AudioManager _audioManager;
	private PlayerForce _playerForce;

	[Inject]
	public void Construct(AudioManager audioManager, PlayerForce playerForce)
	{
		_audioManager = audioManager;
		_playerForce = playerForce;
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
		Application.targetFrameRate = 60;
		screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
		Color color = colorTable[Random.Range(0, colorTable.Length)];
		player.SetColor(color);
		trailMaterial.color = color;
	}

	private void Update()
	{
		if (uIManager.gameState == GameState.PLAYING && Input.GetMouseButton(0) && !uIManager.IsButton() && !spawning)
		{
			spawning = true;
			InitVariables();
			StartCoroutine(SpawnObstacle());
		}
	}

	private void InitVariables()
	{
		currentTimeBetweenObstacles = startTimeBetweenObstacles;
	}

	private IEnumerator SpawnObstacle()
	{
		if (uIManager.gameState == GameState.PAUSED) yield return new WaitUntil(() => uIManager.gameState == GameState.PLAYING);
		
		if (_playerForce.Value > 50f)
		{
			currentTimeBetweenObstacles = minTimeBetweenObstacles;
		}
		else if (_playerForce.Value  > 35f)
		{
			currentTimeBetweenObstacles = startTimeBetweenObstacles - 0.25f;
		}
		else if (_playerForce.Value  > 15f)
		{
			currentTimeBetweenObstacles = startTimeBetweenObstacles - 0.15f;
		}

		var obstacleFromPool = ObstaclesPool.Get.GetRandomObject();
		var renderer = obstacleFromPool.GetComponent<Obstacle>().Renderer;
		
		tempPos = new Vector2(
			Random.Range(0f - screenSize.x + renderer.bounds.size.x, screenSize.x - renderer.bounds.size.x), 
			screenSize.y + renderer.bounds.size.y);
		
		obstacleFromPool.GetComponent<Obstacle>().InitObstacle(tempPos);
		obstacleFromPool.SetActive(true);
		yield return new WaitForSecondsRealtime(currentTimeBetweenObstacles);
		StartCoroutine(SpawnObstacle());
	}
	
	public void RestartGame()
	{
		if (uIManager.gameState == GameState.PAUSED)
		{
			Time.timeScale = 1f;
		}
		uIManager.ShowGameplay();
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
		if (uIManager.gameState == GameState.PLAYING)
		{
			player.gameObject.SetActive(false);
			
			StopAllCoroutines();
			spawning = false;
			_audioManager.PlayEffects(_audioManager.gameOver);
			uIManager.ShowGameOver();
			playerForce.UpdateScoreGameover();
		}
	}
}
