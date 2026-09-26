using System;
using PixelCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.Model.Definitions.Repositories
{
    [CreateAssetMenu(fileName = "PerksItem", menuName = "Defs/PerksItem")]
    public class PerkRepository  : DefRepository<PerkDef>
    {
        
    }

    [Serializable]
    public struct PerkDef :  IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _info;
        [SerializeField] private ItemWithCount _price;


        public ItemWithCount Price => _price;

        public string Info => _info;

        public Sprite Icon => _icon;

        public string Id => _id;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId] [SerializeField] private string _itemId;
        [SerializeField] private int _count;

        public string ItemId => _itemId;

        public int Count => _count;
    }
}