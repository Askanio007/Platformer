using System;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] public InventoryData _inventory;
        public int Hp;
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