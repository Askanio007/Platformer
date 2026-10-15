using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace AloneCrew.Components
{
    public class LightManagerComponent : MonoBehaviour
    {
        [SerializeField] private Light2D _global;
        [SerializeField] private float _globalIntensity;
        [SerializeField] private float _globalIntensityDefault;
        
        [SerializeField] private Light2D[] _light2Ds;
        [SerializeField] private Color _color;
        [SerializeField] private Color _defaultColor;

        private Color currentColor;
        private float currentGlobalIntensity;

        private void Start()
        {
            currentColor = _defaultColor;
            currentGlobalIntensity = _globalIntensityDefault;
        }

        public void ChangeColor()
        {
            currentColor = currentColor == _defaultColor ? _color : _defaultColor;
            foreach (var light2D in _light2Ds)
            {
                light2D.color = currentColor;
            }
            currentGlobalIntensity = currentGlobalIntensity.Equals(_globalIntensityDefault) ? _globalIntensity : _globalIntensityDefault;
            _global.intensity = currentGlobalIntensity;
        }

    }
}