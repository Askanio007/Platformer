using System;
using AloneCrew.Model.Data.Properties;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] public InventoryData _inventory;
        public IntPersistentProperty Hp = new IntPersistentProperty(6);
        public bool IsArmed;
        public InventoryData Inventory => _inventory;
        public PlayerData Clone()
        {
            return new PlayerData
            {
                Hp = Hp,
                IsArmed = IsArmed
            };
        }
    }
}