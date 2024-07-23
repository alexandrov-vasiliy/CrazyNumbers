
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MultiplyObstacle : BaseObstacle, IInteractable
{
    [SerializeField] private TextMeshPro number;
    public override void InitObstacle(Vector2 position, float force, float gravityScale, ObstacleType type)
    {
        base.InitObstacle(position, force, gravityScale, type);

        number.text = $"* {NumberForce}";
        transform.position = position;
    }

    public void Interact()
    {
            _playerForce.MultiplyPlayerForce(NumberForce);
            _playerEvents.ApplyObstacle(_type);
            gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D _)
    {
        spriteRenderer.DOFade(0, 2f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}