using System;
using AloneCrew.Model.Data.Properties;
using UnityEngine;

namespace AloneCrew.Model.Definitions.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager I;

        private StringPersistentProperty _localeKey = new StringPersistentProperty("en", "localization/current");
        private LocaleDef _localeDef;
        public event Action OnLocaleChanged;
        
        public string LocalKey => _localeKey.Value;
        
        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            _localeDef = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _localeKey.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localeDef.Locales.TryGetValue(key, out string locale) ? locale :  $"%%%{key}";
        }

        public void SetLocale(string localKey)
        {
            LoadLocale(localKey);
        }
    }
}