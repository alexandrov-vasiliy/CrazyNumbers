using TMPro;
using UnityEngine;

namespace Obstacles
{
    public class MultiplyObstacle : BonusObstacle, IInteractable
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
    }
}