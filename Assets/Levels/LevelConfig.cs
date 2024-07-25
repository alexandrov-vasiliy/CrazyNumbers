using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "obstacleGame/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public int ObstacleCount => obstacles.FindAll((info) => info.type == ObstacleType.Obstacle).Count;

        public LevelType typeLevel;
        
        [System.Serializable]
        public struct ObstacleInfo
        {
            public float spawnRate;
            public float force;
            public float gravityScale;
            public ObstacleType type;
        }

        public List<ObstacleInfo> obstacles;
        
        
        
    }
}