using GamePush;
using Unity.VisualScripting;

namespace Analytics
{
    public class GamePushAnalytics : IAnalytics, IInitializable
    {
        public void SendGoal(string goalName, string goalValue)
        {
            GP_Analytics.Goal(goalName, goalValue);
        }

        public void SendGoal(string goalName, int goalValue)
        {
            GP_Analytics.Goal(goalName, goalValue);
        }

        public async void Initialize()
        {
            await GP_Init.Ready;
        }
    }
}