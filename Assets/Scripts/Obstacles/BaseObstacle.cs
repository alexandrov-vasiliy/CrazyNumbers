using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class BaseObstacle : MonoBehaviour
{
    
    [SerializeField] protected Rigidbody2D _rb;
    public float NumberForce = 1f;
    public SpriteRenderer spriteRenderer;
    [Inject] protected PlayerForce _playerForce;
    [Inject] protected PlayerEvents _playerEvents;
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
    }
}