using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class Player : MonoBehaviour
{
    public float followSpeed = 2f;

    public float offsetY = 0.8f;

    public SpriteRenderer Renderer;


    [SerializeField, Range(0f, 1f)] private float animationDuration = 0.2f;

    [FormerlySerializedAs("scaleMultiplyer")] [SerializeField]
    private float scaleMultiply = 1.2f;

    private Vector2 destination;

    private Color color;

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
        _playerEvents.OnPlayerDead += GameOver;
        _playerEvents.OnPlayerApplyObstacle += ApplyObstacle;
    }

    private void OnDisable()
    {
        _playerEvents.OnPlayerDead -= GameOver;
        _playerEvents.OnPlayerApplyObstacle -= ApplyObstacle;
    }

    private void ApplyObstacle()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(transform.localScale * scaleMultiply, animationDuration))
            .Append(transform.DOScale(new Vector3(1f, 1f, 1f), animationDuration));

        _audioManager.PlayEffects(_audioManager.sameColor);
    }

    private void GameOver()
    {
        _audioManager.PlayEffects(_audioManager.wrongColor);
        GameManager.Instance.GameOver();
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

    public void SetColor(Color _color)
    {
        color = _color;
        Renderer.color = color;
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
                destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                destination = new Vector2(destination.x, destination.y + offsetY);
                transform.position = Vector2.Lerp(base.transform.position, destination, followSpeed * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(0))
            {
                follow = false;
            }
        }
    }
}