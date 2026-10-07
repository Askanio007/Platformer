using System;
using AloneCrew.Components;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.Boss.Patric
{
    public class HealthAnimationGlue : MonoBehaviour
    {
        [SerializeField] private HealthComponent _hp;
        [SerializeField] private Animator _animator;
        
        public static readonly int Health = Animator.StringToHash("Health");
        
        private CompositeDisposable _trash = new CompositeDisposable();

        public void Awake()
        {
            _trash.Retain(_hp.OnChange.Subscribe(OnHealthChanged));
            OnHealthChanged(_hp.Health);
        }

        private void OnHealthChanged(int health)
        {
            _animator.SetInteger(Health, health);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}