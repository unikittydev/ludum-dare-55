using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace UniOwl.Localization
{
    public class LocaleSwitcher : MonoBehaviour
    {
        private int currentLocaleIndex;
        
        private IEnumerator Start()
        {
            yield return LocalizationSettings.InitializationOperation;

            if (StaticStatsSaver.localeIndex != -1)
            {
                currentLocaleIndex = StaticStatsSaver.localeIndex;
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocaleIndex];
            }
            else
            {
                currentLocaleIndex =
                    LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
                StaticStatsSaver.localeIndex = currentLocaleIndex;
            }
        }

        public void NextLocale()
        {
            currentLocaleIndex = (currentLocaleIndex + 1) % LocalizationSettings.AvailableLocales.Locales.Count;
            StaticStatsSaver.localeIndex = currentLocaleIndex;
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocaleIndex];
        }
    }
}
