using System;
using TMPro;
using UnityEngine;
using Zenject;


public class Obstacle : BaseObstacle
{
    public Color color;
    public TextMeshPro number;
    public SpriteRenderer Renderer;
    

    [SerializeField] private Color ApplyColor;
    [SerializeField] private Color DangerColor;

    private void OnEnable()
    {
        _playerForce.OnPlayerForceUpdate += ChangeColorFromPlayerForce;
    }

    private void OnDisable()
    {
        _playerForce.OnPlayerForceUpdate -= ChangeColorFromPlayerForce;
    }

    public override void InitObstacle(Vector2 position, float force, float gravityScale)
    {
        base.InitObstacle(position, force, gravityScale);

        ChangeColorFromPlayerForce(_playerForce.Value);

        number.text = NumberForce.ToString();
        transform.position = position;
    }


    private void ChangeColorFromPlayerForce(int playerForce)
    {
        color = playerForce >= NumberForce ? ApplyColor : DangerColor;
        Renderer.color = color;
    }

    public void Interact()
    {
        if (NumberForce <= _playerForce.Value)
        {
            _playerForce.IncrementPlayerForce(NumberForce);
            _playerEvents.ApplyObstacle();
            gameObject.SetActive(false);
        }
        else
        {
            _playerEvents.Dead();
        }
    }
}