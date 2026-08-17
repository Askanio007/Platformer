using System;
using AloneCrew.Model.Data.Properties;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Widgets
{
    public class AudioSettingsWidget : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _value;
        
        private FloatPersistentProperty _model;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderChanged);
        }

        public void SetModel(FloatPersistentProperty model)
        {
            _model = model;
            model.OnChanged += OnValueChanged;
            OnValueChanged(model.Value, model.Value);
        }
        
        private void OnValueChanged(float newValue, float oldValue)
        {
            var textValue = Mathf.Round(newValue * 100f);
            _value.text = textValue.ToString();
            _slider.normalizedValue = newValue;
        }
        
        private void OnSliderChanged(float newValue)
        {
            _model.Value = newValue;
        }

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(OnSliderChanged);
            _model.OnChanged -= OnValueChanged;
        }
    }
}