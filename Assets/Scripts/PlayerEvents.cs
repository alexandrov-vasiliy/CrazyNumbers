using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action OnPlayerApplyObstacle;
    public event Action OnPlayerDead;
    
    
    public event Action OnLevelComplete;

    public void ApplyObstacle()
    {
        OnPlayerApplyObstacle?.Invoke();
    }

    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public void Dead()
    {
        OnPlayerDead?.Invoke();
    }
}