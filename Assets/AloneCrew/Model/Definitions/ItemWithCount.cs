using System;
using AloneCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.UI.Windows.Perks
{
    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId] [SerializeField] private string _itemId;
        [SerializeField] private int _count;

        public string ItemId => _itemId;
        public int Count => _count;
    }
}