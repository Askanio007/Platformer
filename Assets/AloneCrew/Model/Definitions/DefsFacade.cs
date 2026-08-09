using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [CreateAssetMenu(fileName = "DefsFacade", menuName = "Defs/DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;
        [SerializeField] private int _inventorySize;
        
        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;
        
        public InventoryItemDef Items => _items;
        public int InventorySize => _inventorySize;

        private static DefsFacade LoadDefs()
        {
            return Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}