using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseObstacle : MonoBehaviour
{
    [SerializeField] private float _divideStep;
    [SerializeField] protected Rigidbody2D _rb;
    public float NumberForce = 1f;
    public SpriteRenderer spriteRenderer;
    [Inject] protected PlayerForce _playerForce;
    [Inject] protected PlayerEvents _playerEvents;
    [Inject] private AudioManager _audioManager;
    protected ObstacleType _type;

    [SerializeField] private Vector2 minMaxRangeCoef;
    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody2D>();
    }
    
    public virtual void InitObstacle(Vector2 position, float force, float gravityScale, ObstacleType type)
    {
        _rb.gravityScale = gravityScale;

        NumberForce = force;
        if (type != ObstacleType.Boss)
        {
            RandomizeSize();
        }
        
        transform.position = position;

        _type = type;
        if (_type == ObstacleType.Boss)
        {
            int bossIndex = Random.Range(0, _audioManager.bossSpawn.Length);
            _audioManager.PlayBossSound();
        }
    }

    private void RandomizeSize()
    {
        transform.localScale = Vector3.one;
        
        transform.localScale += new Vector3(Mathf.Clamp(NumberForce / _divideStep, minMaxRangeCoef.x, minMaxRangeCoef.y),
            Mathf.Clamp(NumberForce / _divideStep, minMaxRangeCoef.x, minMaxRangeCoef.y), 1);
    }
    
}