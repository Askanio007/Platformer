using System;
using AloneCrew.UI.Windows.Perks;
using AloneCrew.Utils; 
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
        [SerializeField] private Cooldown _cooldown;
        [SerializeField] private float _value;


        public ItemWithCount Price => _price;

        public string Info => _info;

        public Sprite Icon => _icon;

        public string Id => _id;
        
        public float Value => _value;
        
        public Cooldown Cooldown => _cooldown;
        
        
    }
}