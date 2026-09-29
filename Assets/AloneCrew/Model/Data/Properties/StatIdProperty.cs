using System;
using AloneCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.Model.Data.Properties
{
    [Serializable]
    public class StatIdProperty : ObservabilityProperty<StatId>
    {
        public StatIdProperty(StatId str) : base(str)
        {
            Init();
        }

        protected override void Write(StatId value)
        {
            _value = value;
        }

        protected override StatId Read(StatId _defaultValue)
        {
            return _value;
        }
    }
}