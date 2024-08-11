using System;
using TMPro;
using UnityEngine;
using Zenject;

[Serializable]
public struct ColorThreshold
{
    public Color color;
    public float threshold;
}

public class Obstacle : BaseObstacle, IInteractable
{
    public TextMeshPro number;
    
    [SerializeField] private ColorThreshold[] colorThresholds;
    
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
        float difference = Mathf.Clamp((NumberForce - playerForce) / 100f * 100f, -100, 100);
    
        // Итерируем по массиву порогов и выбираем соответствующий цвет
        Color color = Color.red;
        foreach (var colorThreshold in colorThresholds)
        {
            if (difference <= colorThreshold.threshold)
            {
                color = colorThreshold.color;
            }
        }

        spriteRenderer.color = color;
    }

    public void Interact()
    {
        
        if (NumberForce <= _playerForce.Value)
        {
            _playerForce.IncrementPlayerForce(NumberForce);
            _playerEvents.ApplyObstacle(_type);
            gameObject.SetActive(false);
            
            if (_type == ObstacleType.Boss)
            {
                _playerEvents.LevelComplete();
            }
        }
        else
        {
            _playerEvents.Dead();
        }
    }
}