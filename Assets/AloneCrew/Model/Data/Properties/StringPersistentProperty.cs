using System;
using UnityEngine;

namespace AloneCrew.Model.Data.Properties
{
    [Serializable]
    public class StringPersistentProperty : PrefsPersistentProperty<string>
    {
        public StringPersistentProperty(string defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override void Write(string value)
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }

        protected override string Read(string _defaultValue)
        {
            return PlayerPrefs.GetString(Key, _defaultValue);
        }
    }
}