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

            currentLocaleIndex =
                LocalizationSettings.AvailableLocales.Locales.IndexOf(LocalizationSettings.SelectedLocale);
        }

        public void NextLocale()
        {
            currentLocaleIndex = (currentLocaleIndex + 1) % LocalizationSettings.AvailableLocales.Locales.Count;

            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocaleIndex];
        }
    }
}
