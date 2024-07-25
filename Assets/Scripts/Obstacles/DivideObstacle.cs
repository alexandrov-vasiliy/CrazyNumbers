using Obstacles;
using TMPro;
using UnityEngine;

public class DivideObstacle : BonusObstacle, IInteractable
{
    
    [SerializeField] private TextMeshPro number;
    public override void InitObstacle(Vector2 position, float force, float gravityScale,ObstacleType type)
    {
        base.InitObstacle(position, force, gravityScale, type);

        number.text = $"/ {NumberForce}";
        transform.position = position;
    }
    
    public void Interact()
    {
            _playerForce.DividePlayerForce(NumberForce);
            _playerEvents.ApplyObstacle(_type);
            gameObject.SetActive(false);
    }

    
}