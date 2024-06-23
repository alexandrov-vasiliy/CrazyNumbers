using UnityEngine;
using YG;

namespace Level
{
    public class YGLevelSaver : ILevelSaver

    {
        /*private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        public void GetLoad()*/
        
        public void SaveLevel(int levelIndex)
        {
            YandexGame.savesData.LevelIndex = levelIndex;
            YandexGame.SaveProgress();
        }

        public int GetSavedLevel()
        {
            return YandexGame.savesData.LevelIndex;
        }
    }
}