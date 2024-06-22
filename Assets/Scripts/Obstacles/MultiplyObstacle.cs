
using TMPro;
using UnityEngine;

public class MultiplyObstacle : BaseObstacle, IInteractable
{
    [SerializeField] private TextMeshPro number;
    public override void InitObstacle(Vector2 position, float force, float gravityScale, InteractableType type)
    {
        base.InitObstacle(position, force, gravityScale, type);

        number.text = $"* {NumberForce}";
        transform.position = position;
    }

    public void Interact()
    {
            _playerForce.MultiplyPlayerForce(NumberForce);
            _playerEvents.ApplyObstacle();
            gameObject.SetActive(false);
    }
}