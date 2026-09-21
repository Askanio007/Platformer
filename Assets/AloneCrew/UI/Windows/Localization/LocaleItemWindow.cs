using System;
using AloneCrew.Model.Definitions.Localization;
using AloneCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.UI.Windows.Localization
{
    public class LocaleItemWindow : MonoBehaviour, IItemRenderer<LocaleItemWindow.LocaleInfo>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelected;

        public void Start()
        {
            LocalizationManager.I.OnLocaleChanged += OnLocaleChanged;
        }

        private void OnLocaleChanged()
        {
            var active = LocalizationManager.I.LocalKey == _data.LocaleId;
            _selector.SetActive(active);
        }

        private LocaleInfo _data;
        public void SetData(LocaleInfo localeKey, int index)
        {
            _data = localeKey;
            OnLocaleChanged();
            _text.text = localeKey.LocaleId.ToUpper();
        }

        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocaleId);
        }

        public class LocaleInfo
        {
            public string LocaleId;
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocaleChanged -= OnLocaleChanged;
        }
    }
    
    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {
            
    }
}