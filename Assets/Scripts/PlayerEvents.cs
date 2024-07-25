using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action<ObstacleType> OnPlayerApplyObstacle;
    public event Action OnPlayerDead;
    public event Action OnLevelComplete;

    [SerializeField] private bool CanDead = false;

    public void ApplyObstacle(ObstacleType type)
    {
        OnPlayerApplyObstacle?.Invoke(type);
    }
    

    public void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }

    public void Dead()
    {
        if (CanDead)
        {
            OnPlayerDead?.Invoke();
        }
    }
}