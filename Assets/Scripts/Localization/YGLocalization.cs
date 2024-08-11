using YG;

namespace Localization
{
    public class YGLocalization : ILocalization
    {
        public string GetLang()
        {
          return YandexGame.lang;
        }
    }
}