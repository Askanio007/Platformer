using System;
using AloneCrew.Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;
        
        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;

        public void Add(GameObject gameObject)
        {
            var hero = gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                var success = hero.AddInInventory(_id, _count);
                if (success)
                {
                    _onSuccess?.Invoke();
                }
                else
                {
                    _onFail?.Invoke();
                }
            }
                
        }
    }
}