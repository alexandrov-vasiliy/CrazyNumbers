using System.Collections.Generic;
using YG;

namespace Analytics
{
    public class YandexMetrikaAnalytics : IAnalytics
    {
        public void CompleteLevel(int level)
        {
            YandexMetrica.Send("CompleteLevel " , new Dictionary<string, string>
            {
                { "levelIndex", level.ToString() }
            });
        }

        public void WatchReward()
        {
            YandexMetrica.Send("WatchReward");
        }
    }
}