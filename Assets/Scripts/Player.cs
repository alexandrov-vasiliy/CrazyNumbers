using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float followSpeed = 2f;

    public float offsetY = 0.8f;

    public SpriteRenderer Renderer;
    
    [Header("Destination")]
    public float offsetMinX = 0f;
    public float offsetMaxX = 0f;
    public float offsetMinY = 0f;
    public float offsetMaxY = 0f;

    
    [Header("Animation")]
    [SerializeField, Range(0f, 1f)] private float animationDuration = 0.2f;

    [FormerlySerializedAs("scaleMultiplyer")] [SerializeField]
    private float scaleMultiply = 1.2f;

    private Vector2 destination;

    private Color _color;

    private bool follow;

    private AudioManager _audioManager;
    private PlayerForce _playerForce;
    private PlayerEvents _playerEvents;
    private UIManager _uiManager;

    [Inject]
    public void Construct(AudioManager audioManager, PlayerForce playerForce, PlayerEvents playerEvents, UIManager uiManager)
    {
        _audioManager = audioManager;
        _playerForce = playerForce;
        _playerEvents = playerEvents;
        _uiManager = uiManager;
    }

    private void OnEnable()
    {
        _playerEvents.OnPlayerApplyObstacle += ApplyObstacle;
    }

    private void OnDisable()
    {
        _playerEvents.OnPlayerApplyObstacle -= ApplyObstacle;
    }

    private void ApplyObstacle(ObstacleType _)
    {
        DOTween.Sequence()
            .Append(transform.DOScale(transform.localScale * scaleMultiply, animationDuration))
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), animationDuration));

        _audioManager.PlayEffects(_audioManager.sameColor);
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_uiManager.gameState != GameState.PLAYING)
        {
            return;
        }

        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact();
        }
    }

    public void SetColor(Color color)
    {
        _color = color;
        Renderer.color = _color;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            follow = true;
        }

        if (_uiManager.gameState == GameState.PLAYING && !_uiManager.IsButton())
        {
            if (Input.GetMouseButton(0) && follow)
            {
                Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                destination = new Vector2(destination.x, destination.y + offsetY);

                // Получаем границы камеры в мировых единицах с учетом смещения
                float minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + offsetMinX;
                float maxX = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x - offsetMaxX;
                float minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y + offsetMinY;
                float maxY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y - offsetMaxY;

                // Ограничиваем destination, чтобы игрок не выходил за пределы экрана
                float clampedX = Mathf.Clamp(destination.x, minX, maxX);
                float clampedY = Mathf.Clamp(destination.y, minY, maxY);

                // Используем ограниченные координаты для назначения следования
                destination = new Vector3(clampedX, clampedY, -3f);

                transform.position = Vector3.Lerp(transform.position, destination, followSpeed * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(0))
            {
                follow = false;
            }
        }
    }
}