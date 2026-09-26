using System.Collections.Generic;
using PixelCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.Model.Definitions.Repositories
{
    public class DefRepository<TDefType> : ScriptableObject where TDefType : IHaveId
    {
        [SerializeField] protected TDefType[] _collection;

        public TDefType Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;

            foreach (var itemDef in _collection)
            {
                if (itemDef.Id == id)
                    return itemDef;
            }

            return default;
        }

        public IList<TDefType> All => new List<TDefType>(_collection).ToArray();
    }
}