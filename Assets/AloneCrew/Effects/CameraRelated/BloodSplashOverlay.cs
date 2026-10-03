using System;
using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.Effects.CameraRelated
{
    public class BloodSplashOverlay : MonoBehaviour
    {
        [SerializeField] private Transform _overlay;
        
        private static readonly int Health = Animator.StringToHash("Health");
        
        private Animator _animator;
        private Vector3 _overScale;
        private GameSession _session;
        
        private CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _overScale = _overlay.localScale - Vector3.one;
            _session = FindFirstObjectByType<GameSession>();
            _trash.Retain(_session.Data.Hp.SubscribeAndInvoke(InHpChanged));
        }

        private void InHpChanged(int newValue, int oldValue)
        {
            var maxHp = _session.StatsModel.GetCurrentValue(StatId.Hp);
            var hpNormalized = newValue / maxHp;
            _animator.SetFloat(Health, hpNormalized);
            
            var overlayModifier = Mathf.Max(hpNormalized - 0.3f, 0);
            _overlay.localScale = Vector3.one + _overScale * overlayModifier;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}