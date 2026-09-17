using System;
using System.Linq;
using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [CreateAssetMenu(fileName = "InventoryItems", menuName = "Defs/InventoryItems")]
    public class InventoryItemDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] _items;

        public ItemDef Get(string id)
        {
            foreach (var itemDef in _items)
            {
                if (itemDef.Id == id) return itemDef;
            }

            return default;
        }
        
#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => _items;
#endif

    }

[Serializable]
    public struct ItemDef
    {
        [SerializeField] private string _id;
        [SerializeField] private ItemTag[] _tags;
        [SerializeField] private Sprite _icon;
        public string Id => _id;
        public ItemTag[]  ItemTags => _tags;
        public Sprite Icon => _icon;
        public bool IsVoid => string.IsNullOrEmpty(_id);

        public bool HasTag(ItemTag tag)
        {
            return _tags.Contains(tag);
        }
    }
}