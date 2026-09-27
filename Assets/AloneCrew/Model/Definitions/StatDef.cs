using System;
using AloneCrew.Model.Data;
using AloneCrew.Model.Definitions.Repositories;
using UnityEngine;

namespace AloneCrew.Model.Definitions
{
    [Serializable]
    public class StatDef
    {
        [SerializeField] private StatId _id;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatLevel[] _levels;

        public StatLevel[] Levels => _levels;

        public Sprite Icon => _icon;

        public string Name => _name;

        public StatId ID => _id;
    }

    [Serializable]
    public struct StatLevel
    {
        [SerializeField] private float _value;
        [SerializeField] private ItemWithCount _price;

        public ItemWithCount Price => _price;

        public float Value => _value;
    }

    public enum StatId
    {
        Hp,
        Speed,
        RangeDamage
    }
}