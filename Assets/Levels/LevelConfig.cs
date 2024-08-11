using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    
    [Serializable]
    public struct Tutorial
    {
        [TextArea] public string RuText;
        [TextArea] public string EnText;
    }
    
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "obstacleGame/LevelConfig", order = 0)]
    
    public class LevelConfig : ScriptableObject
    {
        public int ObstacleCount => obstacles.FindAll((info) => info.type == ObstacleType.Obstacle).Count;

        public LevelType typeLevel;
        
        [Serializable]
        public struct ObstacleInfo
        {
            public float spawnRate;
            public float force;
            public float gravityScale;
            public ObstacleType type;
        }
        
        public List<ObstacleInfo> obstacles;

        public Tutorial Tutorial;

    }
}