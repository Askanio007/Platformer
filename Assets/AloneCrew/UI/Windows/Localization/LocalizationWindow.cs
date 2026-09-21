using System.Collections.Generic;
using AloneCrew.Model.Definitions.Localization;
using AloneCrew.UI.Widgets;
using UnityEngine;

namespace AloneCrew.UI.Windows.Localization
{
    public class LocalizationWindow : AnimatedWindow
    {
        private DataGroup<LocaleItemWindow.LocaleInfo, LocaleItemWindow> _dataGroup;
        [SerializeField] private Transform _container;
        [SerializeField] private LocaleItemWindow _prefab;

        private string[] _locales = new[] { "en", "ru", "es" };

        protected override void Start()
        {
            base.Start();   
            _dataGroup = new DataGroup<LocaleItemWindow.LocaleInfo, LocaleItemWindow>(_prefab, _container);
            _dataGroup.SetData(ComposeData());
        }

        private List<LocaleItemWindow.LocaleInfo> ComposeData()
        {
            var data = new List<LocaleItemWindow.LocaleInfo>();
            foreach (var locale in _locales)
            {
                data.Add(new LocaleItemWindow.LocaleInfo
                {
                    LocaleId = locale
                });
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.I.SetLocale(selectedLocale);
        }
    
    
    }
}