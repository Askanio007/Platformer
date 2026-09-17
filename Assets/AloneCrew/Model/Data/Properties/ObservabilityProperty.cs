using System;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.Model.Data.Properties
{
    public abstract class ObservabilityProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;
        private TPropertyType _defaultValue;
        
        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;
        
        public IDisposable Subscribe(OnPropertyChanged call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public IDisposable SubscribeAndInvoke(OnPropertyChanged call)
        {
            OnChanged += call;
            var dispose = new ActionDisposable(() => OnChanged -= call);
            call(_value, _value);
            return dispose;
        }

        public ObservabilityProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

        public TPropertyType Value
        {
            get => _value;
            set
            {
                if (!_value.Equals(value))
                {
                    var oldValue = _value;
                    Write(value);
                    _value = value;
                    OnChanged?.Invoke(_value, oldValue);
                }
            }
        }

        protected void Init()
        {
            _value = Read(_defaultValue);
        }
        
        protected void InvokeChangedEvent(TPropertyType newValue, TPropertyType oldValue)
        {
            OnChanged?.Invoke(newValue, oldValue);
        }
        
        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType _defaultValue);

    }
}