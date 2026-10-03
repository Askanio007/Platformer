using System;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _cooldown;
        private float _timesUp;
        public event Action<float> OnChanged;

        public void Reset()
        {
            _timesUp = Time.time + _cooldown;
            OnChanged?.Invoke(_cooldown);
        }

        public bool IsReady()
        {
            return _timesUp <= Time.time;
        }
        
        public IDisposable Subscribe(Action<float> call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }
        
    }
}