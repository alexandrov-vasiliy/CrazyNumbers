using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseObstacle : MonoBehaviour
{
    
    [SerializeField] protected Rigidbody2D _rb;
    public float NumberForce = 1f;
    public SpriteRenderer spriteRenderer;
    [Inject] protected PlayerForce _playerForce;
    [Inject] protected PlayerEvents _playerEvents;
    [Inject] private AudioManager _audioManager;
    protected InteractableType _type;
    
    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody2D>();
    }
    
    public virtual void InitObstacle(Vector2 position, float force, float gravityScale, InteractableType type)
    {
        _rb.gravityScale = gravityScale;

        NumberForce = force;

        transform.position = position;

        _type = type;
        if (_type == InteractableType.Boss)
        {
            int bossIndex = Random.Range(0, _audioManager.bossSpawn.Length);
            Debug.Log($"Play boss sound {bossIndex}");
            _audioManager.PlayBossSound();
        }
    }
    
}