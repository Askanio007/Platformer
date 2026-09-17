using System;
using System.Collections.Generic;
using System.Linq;
using AloneCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();
        
        public delegate void OnInventoryChanged(string id, int value);
        
        public OnInventoryChanged onInventoryChanged;

        public bool Add(string id, int value)
        {
            if(value <= 0 || DefsFacade.I.InventorySize <= _inventory.Count) return false;

            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return false; 
            var item = GetItem(id);
            if (item == null || !itemDef.HasTag(ItemTag.Stackable))
            {
                item = new  InventoryItemData(id, value);
                _inventory.Add(item);
            }
            else
            {
                item.Value += value;
            }
            onInventoryChanged?.Invoke(id, Count(id));
            return true;
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if (itemDef.IsVoid) return; 
            var item = GetItem(id);
            if (item == null) return;
            item.Value -= value;

            if (item.Value <= 0)
            {
                _inventory.Remove(item);
            }
            onInventoryChanged?.Invoke(id, Count(id));
        }

        public int Count(string id)
        {
            var count = 0;
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id)
                    count += itemData.Value;
            }
            return count;
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var itemData in _inventory)
            {
                if (itemData.Id == id) return itemData;
            }
            return null;
        }

        public InventoryItemData[] GetAll(params ItemTag[] tags)
        {
            var ret = new List<InventoryItemData>();
            foreach (var inv in _inventory)
            {
                var itemDef = DefsFacade.I.Items.Get(inv.Id);
                var isAllRequirements = tags.All(tag  => itemDef.HasTag(tag));
                if (isAllRequirements)
                {
                    ret.Add(inv);
                }
                
            }
            return ret.ToArray();
        }
        
    }
}