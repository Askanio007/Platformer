using System;
using System.Linq;
using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [CreateAssetMenu(fileName = "PotionItems", menuName = "Defs/PotionItems")]
    public class PotionItemsDef : ScriptableObject
    {
        [SerializeField] public PotionDef[] _items;

        public PotionDef Get(string id)
        {
            foreach (var item in _items)
            {
                if (item.Id == id)
                    return item;
            }

            return default;
        }
        
    }

    [Serializable]
    public struct PotionDef
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private PotionItemTag _tag;
        [SerializeField] private int _value;

        public string Id => _id;
        public PotionItemTag Tag => _tag;
        public int Value => _value;
    }
}