using System;
using System.Collections.Generic;
using AloneCrew.Components;
using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Interaction
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private List<RequireItemDef> _items;
        
        private Dictionary<string, RequireItemDef> _defs;

        private void Start()
        {
            _defs =  new Dictionary<string, RequireItemDef>();
            foreach (var def in _items)
            {
                _defs.Add(def.Id, def); 
            }
        }

        public void Check(string inventoryItemId)
        {
            RequireItemDef def;
            if (!_defs.TryGetValue(inventoryItemId, out def))
            {
                return;
            }
            var session = FindFirstObjectByType<GameSession>();
            var defs = DefsFacade.I.Items.Get(inventoryItemId);
            var numItems = session.Data.Inventory.Count(inventoryItemId);
            if (numItems >= def.Count)
            {
                if (def.RemoveAfterUse)
                {
                    session.Data.Inventory.Remove(inventoryItemId, def.Count);
                }
                def.OnSuccess?.Invoke(defs.Value);
            }
            else
            {
                def.OnFail?.Invoke();
            }

        }
        
        
        [Serializable]
        public struct RequireItemDef
        {
            [InventoryId] [SerializeField] private string _id;
            [SerializeField] private int _count;
            [SerializeField] private bool _removeAfterUse;
        
            [SerializeField] private IntChange _onSuccess;
            [SerializeField] private UnityEvent _onFail;
            public string Id => _id;
            public int Count => _count;
            public bool RemoveAfterUse => _removeAfterUse;
            public IntChange OnSuccess => _onSuccess;
            public UnityEvent OnFail => _onFail;
        }
        
    }
}