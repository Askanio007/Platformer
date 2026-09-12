using System;
using System.Collections.Generic;
using AloneCrew.Model;
using AloneCrew.Model.Data;
using AloneCrew.Utils.Disposables;
using UnityEngine;

namespace AloneCrew.UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;

        private GameSession _session;
        private List<InventoryItemWidget> _createdItems = new List<InventoryItemWidget>();
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
            _trash.Retain(_session.QuickInventoryModel.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = _session.QuickInventoryModel.Inventory;
            
            //create items
            for (var i = _createdItems.Count - 1; i < inventory.Length; i++)
            {
                var item = Instantiate(_prefab, _container);
                _createdItems.Add(item);
            }
            
            //update data
            for (int i = 0; i < inventory.Length; i++)
            {
                _createdItems[i].SetData(inventory[i], i);
                _createdItems[i].gameObject.SetActive(true);
            }
            
            //hide unused
            for (int i = inventory.Length; i < _createdItems.Count; i++)
            {
                _createdItems[i].gameObject.SetActive(false);
            }
        }

        public void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}