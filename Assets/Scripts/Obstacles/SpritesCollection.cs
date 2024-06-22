using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{
    [CreateAssetMenu(fileName = "SpritesCollection", menuName = "obstacleGame/SpritesCollection", order = 0)]
    public class SpritesCollection : ScriptableObject
    {
        public List<Sprite> Sprites;
    }
}