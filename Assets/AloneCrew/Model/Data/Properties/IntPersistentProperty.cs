using System;
using UnityEngine;

namespace AloneCrew.Model.Data.Properties
{
    [Serializable]
    public class IntPersistentProperty : ObservabilityProperty<int>
    {
        public IntPersistentProperty(int defaultValue) : base(defaultValue)
        {
            Init();
        }

        protected override void Write(int value)
        {
            _value = value;
        }

        protected override int Read(int _defaultValue)
        {
            return _value;
        }
    }
}