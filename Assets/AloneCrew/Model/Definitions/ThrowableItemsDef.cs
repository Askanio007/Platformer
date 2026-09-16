using System;
using System.Linq;
using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [CreateAssetMenu(fileName = "ThrowableItems", menuName = "Defs/ThrowableItems")]
    public class ThrowableItemsDef : ScriptableObject
    {
        [SerializeField] public ThrowableDef[] _items;

        public ThrowableDef Get(string id)
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
    public struct ThrowableDef
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;
        [SerializeField] private int _minAfterThrow;

        public string Id => _id;
        public GameObject Projectile => _projectile;
        public int MinAfterThrow => _minAfterThrow;
    }
}