using System;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions.Localization;
using TMPro;
using UnityEngine;

namespace AloneCrew.UI.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private bool _capitalize;
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            LocalizationManager.I.OnLocaleChanged += OnLocaleChanged;
            Localize();
        }

        public void OnLocaleChanged()
        {
            Localize();
        }

        private void Localize()
        {
            var localization = LocalizationManager.I.Localize(_key);
            _text.text = _capitalize ? localization.ToUpper() : localization;
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}