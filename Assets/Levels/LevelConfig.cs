using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "obstacleGame/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        [System.Serializable]
        public struct ObstacleInfo
        {
            public float spawnRate;
            public float force;
            public float gravityScale;
            public InteractableType type;
        }

        public ObstacleInfo[] obstacles;
    }
}