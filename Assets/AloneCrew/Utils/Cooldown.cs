using System;
using UnityEngine;

namespace AloneCrew.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _cooldown;
        private float _timesUp;

        public void Reset()
        {
            _timesUp = Time.time + _cooldown;
        }

        public bool IsReady()
        {
            return _timesUp <= Time.time;
        }
        
    }
}