namespace Level
{
    public interface ILevelSaver
    {
        public void SaveLevel(int levelIndex);
        public int GetSavedLevel();
    }
}