using System.Collections;
using Levels;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class ObstacleSpawner : MonoBehaviour
{
    public LevelConfig levelConfig; // Переменная для хранения конфигурации уровня

    private Camera _camera;
    private Vector2 _screenBounds;
    private IObjectFactory _factory;

    public UnityEvent OnLevelComplete = new UnityEvent();

    [Inject] private UIManager _uiManager;

    private void Start()
    {
        _factory = new PoolObjectFactory();
        _camera = Camera.main;
        _screenBounds =
            _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _camera.transform.position.z));
        
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnObstacles());
    }
    

    private IEnumerator SpawnObstacles()
    {
        yield return new WaitUntil(() => _uiManager.gameState == GameState.PLAYING);

        foreach (var obstacleInfo in levelConfig.obstacles)
        {
            var obstacleFromPool = _factory.CreateObject(obstacleInfo.type);
            BaseObstacle obstacleScript = obstacleFromPool.GetComponent<BaseObstacle>();

            if (obstacleScript == null) yield return null;

            if (obstacleFromPool != null)
            {
                Vector2 spawnPosition = CalculateSpawnPosition(obstacleInfo, obstacleScript);
                obstacleFromPool.SetActive(true);
                obstacleScript.InitObstacle(spawnPosition, obstacleInfo.force, obstacleInfo.gravityScale);
            }

            yield return new WaitForSeconds(obstacleInfo.spawnRate);
        }

        Debug.Log("Level Complete");
    }

    private Vector2 CalculateSpawnPosition(LevelConfig.ObstacleInfo obstacleInfo, BaseObstacle obstacleScript)
    {
        float spawnPositionX = Random.Range(
            -_screenBounds.x + obstacleScript.spriteRenderer.bounds.extents.x, // Используем extents.x для получения половины ширины спрайта
            _screenBounds.x - obstacleScript.spriteRenderer.bounds.extents.x); // Используем extents.x для получения половины ширины спрайта

        // Рассчитываем координаты по Y так, чтобы препятствия спавнились чуть выше видимой области экрана
        float spawnPositionY = _screenBounds.y + obstacleScript.spriteRenderer.bounds.extents.y; // Используем extents.y для получения половины высоты спрайта

        Vector2 spawnPosition = new Vector2(spawnPositionX, spawnPositionY);

        if (obstacleInfo.type == InteractableType.Boss)
        {
            float centerX = _camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).x;

            // Получаем координату за верхней границей видимости камеры по высоте
            float topY = _screenBounds.y + obstacleScript.spriteRenderer.bounds.size.y;

            spawnPosition = new Vector2(centerX, topY);
        }

        return spawnPosition;
    }
}