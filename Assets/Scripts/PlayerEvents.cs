using System;
using Adver;
using UnityEngine;
using Zenject;

public class PlayerEvents : MonoBehaviour
{
    
    [Inject] private IAd _ad;
    
    public event Action<ObstacleType> OnPlayerApplyObstacle;
    public event Action OnPlayerDead;
    public event Action OnLevelComplete;

    private int _dieCount = 0;
    [SerializeField] private int _dieAdStep = 2; 
    
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
            _dieCount++;

            OnPlayerDead?.Invoke();
            if (_dieCount >= _dieAdStep)
            {
                _ad.ShowFullScreenAd();
                _dieCount = 0;
            }
        }
        
    }
}