using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [CreateAssetMenu(fileName = "DefsFacade", menuName = "Defs/DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;
        [SerializeField] private PlayerDef _player;
        
        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;
        
        public InventoryItemDef Items => _items;
        public PlayerDef Player => _player;
        public int InventorySize => _player.InventorySize;

        private static DefsFacade LoadDefs()
        {
            return Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}