using GamePush;
using Unity.VisualScripting;

namespace Analytics
{
    public class GamePushAnalytics : IAnalytics, IInitializable
    {
        public async void Initialize()
        {
            await GP_Init.Ready;
        }

        public void CompleteLevel(int level)
        {
            GP_Analytics.Goal("Level Complete", level);
        }

        public void WatchReward()
        {
            GP_Analytics.Hit("Watch Reward");

        }
    }
}