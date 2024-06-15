

using TMPro;
using UnityEngine;

public class DivideObstacle : BaseObstacle, IInteractable
{
    
    [SerializeField] private TextMeshPro number;
    public override void InitObstacle(Vector2 position, float force, float gravityScale)
    {
        base.InitObstacle(position, force, gravityScale);

        number.text = $"/ {NumberForce}";
        transform.position = position;
    }
    
    public void Interact()
    {
            _playerForce.DividePlayerForce(NumberForce);
            _playerEvents.ApplyObstacle();
            gameObject.SetActive(false);
    }
}