using UnityEngine;

namespace AloneCrew.Model.Data.Properties
{
    public abstract class ObservabilityProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;
        private TPropertyType _defaultValue;
        
        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;

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
        
        protected abstract void Write(TPropertyType value);
        protected abstract TPropertyType Read(TPropertyType _defaultValue);

    }
}