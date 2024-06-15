using System.Collections;
using Levels;
using UnityEngine;
using Zenject;

public class ObstacleSpawner : MonoBehaviour
{
    public LevelConfig levelConfig; // Переменная для хранения конфигурации уровня

    [SerializeField] private float obstacleBoundSize = 10f;
    private Camera _camera;
    private Vector2 _screenBounds;
    private IObjectFactory _factory;

    [Inject] private UIManager _uiManager;
    private void Start()
    {
       _factory = new PoolObjectFactory(); 
        _camera = Camera.main;
        _screenBounds = _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));

        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        yield return new WaitUntil(() => _uiManager.gameState == GameState.PLAYING);
        
        foreach (var obstacleInfo in levelConfig.obstacles)
        {
            var obstacleFromPool = _factory.CreateObject(obstacleInfo.type);
            Vector2 spawnPosition = new Vector2(Random.Range(-_screenBounds.x + obstacleBoundSize, _screenBounds.x - obstacleBoundSize), _screenBounds.y);

            if (obstacleFromPool != null) 
            {
                obstacleFromPool.transform.position = spawnPosition;
                obstacleFromPool.SetActive(true); 

                BaseObstacle obstacleScript = obstacleFromPool.GetComponent<BaseObstacle>();

               
                if (obstacleScript != null)
                {
                    obstacleScript.InitObstacle(spawnPosition, obstacleInfo.force, obstacleInfo.gravityScale);
                }
            }
            
            yield return new WaitForSeconds(obstacleInfo.spawnRate); 
        }
        
        Debug.Log("Level Complete");
    }
}