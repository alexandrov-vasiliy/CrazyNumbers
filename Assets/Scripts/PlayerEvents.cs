using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action OnPlayerApplyObstacle;
    public event Action OnPlayerDead;

    public void ApplyObstacle()
    {
        OnPlayerApplyObstacle?.Invoke();
    }

    public void Dead()
    {
        OnPlayerDead?.Invoke();
    }
}