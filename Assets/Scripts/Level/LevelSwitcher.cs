using System.Collections.Generic;
using Levels;
using UnityEngine;
using Zenject;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private List<LevelConfig> _levels;

    [Inject] private ObstacleSpawner _obstacleSpawner;
    private int _currentLevelIndex;
    
    
    
}