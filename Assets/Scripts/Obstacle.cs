using System;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    public Color color;
    public TextMeshPro number;
    public bool hit;
    public SpriteRenderer Renderer;
    public int NumberForce = 1;
    
    [SerializeField] private Color ApplyColor;
    [SerializeField] private Color DangerColor;

    [SerializeField] private int _playerForceMultiplyerUp = 2;
    [SerializeField] private int _playerForceOffserDown = 30;
    [SerializeField] private float _minGravityScale = 0.5f;
    [SerializeField] private float _maxGravityScale = 2.0f;
    [SerializeField] private int _minPlayerForce = 0;
    [SerializeField] private int _maxPlayerForce = 100;

    private Rigidbody2D _rb;
    [Inject] private PlayerForce _playerForce;

    private void OnEnable()
    {
        _playerForce.OnPlayerForceUpdate += ChangeColorFromPlayerForce;
    }

    private void OnDisable()
    {
        _playerForce.OnPlayerForceUpdate -= ChangeColorFromPlayerForce;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hit && (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Line")))
        {
            hit = true;
        }
    }

    public void InitObstacle(Vector2 _position)
    {
        ChangeGravityScale();

        int playerForceValue = _playerForce.Value;
     
        if (playerForceValue <= 3)
        {
            NumberForce = 1;
        }
        else if (playerForceValue <= _playerForceOffserDown)
        {
            NumberForce = Random.Range(1, playerForceValue * _playerForceMultiplyerUp);
        }
        else
        {
            NumberForce = Random.Range(playerForceValue - _playerForceOffserDown, playerForceValue * _playerForceMultiplyerUp);
        }

        ChangeColorFromPlayerForce(playerForceValue);
        
        number.text = NumberForce.ToString();
        transform.position = _position;
    }

    private void ChangeGravityScale()
    {
        int playerForce = _playerForce.Value;
        
        playerForce = Mathf.Clamp(playerForce, _minPlayerForce, _maxPlayerForce);

        // Масштабируем gravityScale между minGravityScale и maxGravityScale
        _rb.gravityScale = _minGravityScale + (_maxGravityScale - _minGravityScale) *
            ((float)playerForce - _minPlayerForce) / (_maxPlayerForce - _minPlayerForce);
    }

    private void ChangeColorFromPlayerForce(int playerForce)
    {
        color = playerForce >= NumberForce ? ApplyColor : DangerColor;
        Renderer.color = color;
    }
}