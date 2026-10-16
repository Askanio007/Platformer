using System;
using System.Collections;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.Components
{
    [RequireComponent(typeof(HealthComponent))]
    public class ImmuneAfterHit : MonoBehaviour
    {
        [SerializeField] private float _immuneTime;
        private HealthComponent _health;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        
        private Coroutine _coroutine;

        private void Awake()
        {
            _health = GetComponent<HealthComponent>();
            _trash.Retain(_health.OnDamage.Subscribe(OnDamage));
        }

        private void OnDamage()
        {
            TryStop();
            if (_coroutine != null) return;
            if (_immuneTime > 0)
                _coroutine = StartCoroutine(MakeImmune());
        }

        private void TryStop()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator MakeImmune()
        {
            _health.DamageLock.Retain(this);
            yield return new WaitForSeconds(_immuneTime);
            _health.DamageLock.Release(this);
        }

        private void OnDestroy()
        {
            TryStop();
            _trash.Dispose();
        }
    }
}