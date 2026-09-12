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
        [InventoryId] [SerializeField] private List<string> _id;
        [SerializeField] private int _count;
        [SerializeField] private bool _removeAfterUse;
        
        [SerializeField] private HealthChange _onSuccess;
        [SerializeField] private UnityEvent _onFail;

        public void Check(string inventoryItemId)
        {
            if (!_id.Contains(inventoryItemId))
            {
                return;
            }
            var session = FindFirstObjectByType<GameSession>();
            var defs = DefsFacade.I.Items.Get(inventoryItemId);
            var numItems = session.Data.Inventory.Count(inventoryItemId);
            if (numItems >= _count)
            {
                if (_removeAfterUse)
                {
                    session.Data.Inventory.Remove(inventoryItemId, _count);
                }
                _onSuccess?.Invoke(defs.Value * (-1));
            }
            else
            {
                _onFail?.Invoke();
            }

        }
        
    }
}