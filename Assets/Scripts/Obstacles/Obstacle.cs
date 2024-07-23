using System;
using TMPro;
using UnityEngine;
using Zenject;


public class Obstacle : BaseObstacle, IInteractable
{
    public Color color;
    public TextMeshPro number;

    [SerializeField] private Color ApplyColor;
    [SerializeField] private Color DangerColor;
    
    [Inject]
    private LevelSwitcher _levelSwitcher;


    
    private void OnEnable()
    {
        _playerForce.OnPlayerForceUpdate += ChangeColorFromPlayerForce;
    }

    private void OnDisable()
    {
        _playerForce.OnPlayerForceUpdate -= ChangeColorFromPlayerForce;
    }

    public override void InitObstacle(Vector2 position, float force, float gravityScale, ObstacleType type)
    {
        base.InitObstacle(position, force, gravityScale, type);
        
        
        
        ChangeColorFromPlayerForce(_playerForce.Value);

        number.text = NumberForce.ToString();
        transform.position = position;
    }


    private void ChangeColorFromPlayerForce(int playerForce)
    {
        color = playerForce >= NumberForce ? ApplyColor : DangerColor;
        spriteRenderer.color = color;
    }

    public void Interact()
    {
        if (NumberForce <= _playerForce.Value)
        {
            _playerForce.IncrementPlayerForce(NumberForce);
            _playerEvents.ApplyObstacle(_type);
            gameObject.SetActive(false);
            
        }
        else
        {
            _playerEvents.Dead();
        }
    }
}