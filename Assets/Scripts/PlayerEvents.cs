using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action<ObstacleType> OnPlayerApplyObstacle;
    public event Action OnPlayerDead;
    public event Action OnLevelComplete;
    
        public event Action<bool> CanDeadChange;

        [SerializeField] private bool _canDead = true;

        public bool CanDead
        {
            get
            {
                return _canDead;
            }
            set
            {
                _canDead = value;
                CanDeadChange?.Invoke(value);
            } 
        }

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
        if (_canDead)
        {
            OnPlayerDead?.Invoke();
        }
    }
}