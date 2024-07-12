using UnityEngine;

namespace Level
{
    public class PlayerPrefsSaver: ILevelSaver
    {
        private const string levelKey = "CN_level";
        public void SaveLevel(int levelIndex)
        {
            PlayerPrefs.SetInt(levelKey, levelIndex);
        }

        public int GetSavedLevel()
        {
            return PlayerPrefs.GetInt(levelKey);
        }
    }
}