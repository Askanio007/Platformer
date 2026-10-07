using System;
using AloneCrew.Components;
using AloneCrew.UI.Widgets;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.Boss.Patric
{
    public class BossHpWidget :  MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private ProgressBarWidget _hpBar;
        [SerializeField] private CanvasGroup _canvas;

        private CompositeDisposable _trash = new CompositeDisposable();
        private float _maxHealth;

        public void Start()
        {
            _maxHealth = _healthComponent.MaxHealth;
            _trash.Retain(_healthComponent.OnChange.Subscribe(OnHpChange));
            _trash.Retain(_healthComponent.OnDie.Subscribe(HideUi));
        }
        
        public void ShowUi()
        {
            _canvas.alpha = 1;
        }

        public void HideUi()
        {
            _canvas.alpha = 0;
        }

        public void OnHpChange(int health)
        {
            float newBarValue = health / _maxHealth;
            _hpBar.SetProgress(newBarValue);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}