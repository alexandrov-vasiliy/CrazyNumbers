using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Obstacle : MonoBehaviour
{
    public Color color;
    public Text number;
    public bool hit;

    public int NumberForce = 1;
    
    [SerializeField] private int _playerForceMultiplyerUp = 4;
    [SerializeField] private int _playerForceOffserDown = 30;
    [SerializeField] private float _minGravityScale = 0.5f;
    [SerializeField] private float _maxGravityScale = 2.0f;
    [SerializeField] private int _minPlayerForce = 0;
    [SerializeField] private int _maxPlayerForce = 100;

    private Rigidbody2D _rb;
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

    public void InitObstacle(Vector2 _position, Color _color)
    {
        ChangeGravityScale();
        
        color = _color;
        GetComponent<SpriteRenderer>().color = _color;
        int playerForce = ScoreManager.Instance.PlayerForce;
        if (playerForce <= 4)
        {
            NumberForce = 1;
        }
        else if (playerForce <= _playerForceOffserDown)
        {
            NumberForce = Random.Range(1, playerForce * _playerForceMultiplyerUp);
        }
        else
        {
            NumberForce = Random.Range(playerForce - _playerForceOffserDown, playerForce * _playerForceMultiplyerUp);
        }

        number.text = NumberForce.ToString();
        transform.position = _position;
    }

    private void ChangeGravityScale()
    {
        int playerForce = ScoreManager.Instance.PlayerForce;
        
        playerForce = Mathf.Clamp(playerForce, _minPlayerForce, _maxPlayerForce);

        // Масштабируем gravityScale между minGravityScale и maxGravityScale
        _rb.gravityScale = _minGravityScale + (_maxGravityScale - _minGravityScale) *
            ((float)playerForce - _minPlayerForce) / (_maxPlayerForce - _minPlayerForce);
    }
}