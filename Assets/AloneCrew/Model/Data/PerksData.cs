using System;
using System.Collections.Generic;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    [Serializable]
    public class PerksData
    {
        [SerializeField] private StringProperty _used = new StringProperty("double-jump");
        [SerializeField] private List<string> _unlocked;
        
        public StringProperty Used => _used;

        public float GetUsedValue()
        {
            return DefsFacade.I.Perks.Get(_used.Value).Value;
        }
        
        public bool IsReady()
        {
            return GetCooldown().IsReady();
        }

        public IDisposable SubscribeOnCooldown(Action<float> call)
        {
            var c = GetCooldown();
            return c != null ? c.Subscribe(call) : null;
        }
        
        public void ResetCooldown()
        {
            GetCooldown().Reset();
        }

        private Cooldown GetCooldown()
        {
            return DefsFacade.I.Perks.Get(_used.Value).Cooldown;
        }

        public void AddPerk(string id)
        {
            if (!_unlocked.Contains(id))
                _unlocked.Add(id);
        }

        public bool IsUnlocked(string id)
        {
            return _unlocked.Contains(id);
        }
    }
}