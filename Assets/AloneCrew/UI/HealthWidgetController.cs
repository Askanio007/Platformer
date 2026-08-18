using System;
using AloneCrew.Model;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
using AloneCrew.Components;
using AloneCrew.UI.Widgets;
using UnityEngine;

namespace AloneCrew.UI.Hud
{
    public class HealthWidgetController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        [SerializeField] private HealthComponent _healthComponent;
        private GameSession _session;

        public void Start()
        {
            OnHealthChanged(_healthComponent.Health);
        }

        public void OnHealthChanged(int newValue)
        {
            var maxHealth = _healthComponent.MaxHealth;
            var val = (float) newValue/maxHealth; 
            _healthBar.SetProgress(val);
            
        }
    }
}