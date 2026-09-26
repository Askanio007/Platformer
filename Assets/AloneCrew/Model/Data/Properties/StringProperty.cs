using System;
using UnityEngine;

namespace AloneCrew.Model.Data.Properties
{
    [Serializable]
    public class StringProperty : ObservabilityProperty<string>
    {
        public StringProperty(string str) : base(str)
        {
            Init();
        }

        protected override void Write(string value)
        {
            _value = value;
        }

        protected override string Read(string _defaultValue)
        {
            return _value;
        }
    }
}