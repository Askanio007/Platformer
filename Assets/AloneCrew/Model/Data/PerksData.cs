using System;
using System.Collections.Generic;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
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
            return DefsFacade.I.Perks.Get(_used.Value).Cooldown.IsReady();
        }
        
        public void ResetCooldown()
        {
            DefsFacade.I.Perks.Get(_used.Value).Cooldown.Reset();
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