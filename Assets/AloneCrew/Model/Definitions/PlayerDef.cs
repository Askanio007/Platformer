using System.Linq;
using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [CreateAssetMenu(fileName = "PlayerDef", menuName = "Defs/PlayerDef")]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;
        [SerializeField] private StatDef[] _stats;
        
        public int InventorySize => _inventorySize;
        public int MaxHealth => _maxHealth;
        
        public StatDef[] Stats => _stats;
        
        public StatDef GetStat(StatId statId) => _stats.FirstOrDefault(x => x.ID == statId);
    }
}