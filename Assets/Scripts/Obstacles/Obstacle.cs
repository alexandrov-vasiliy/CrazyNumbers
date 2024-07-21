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

    public override void InitObstacle(Vector2 position, float force, float gravityScale, InteractableType type)
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
            _playerEvents.ApplyObstacle();
            gameObject.SetActive(false);

            if (_type == InteractableType.Obstacle)
            {
                _levelSwitcher._obstacleReceived++;
            }

            if (_levelSwitcher._obstacleReceived == _levelSwitcher._obstacleCount)
            {
                _levelSwitcher.StartShowLevelComplete();
                _levelSwitcher._obstacleReceived = 0;
            }
            
        }
        else
        {
            _playerEvents.Dead();
        }
    }
}