using System;
using AloneCrew.UI.Windows.Perks;
using UnityEngine;

namespace AloneCrew.Model.Definitions.Repositories
{
    [CreateAssetMenu(fileName = "CraftableItem", menuName = "Defs/CraftableItem")]
    public class CraftableRepository  : DefRepository<CraftableDef>
    {
        
    }

    [Serializable]
    public struct CraftableDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private ItemWithCount[] _price;
        public ItemWithCount[] Price => _price;
        public string Id => _id;
    }
}