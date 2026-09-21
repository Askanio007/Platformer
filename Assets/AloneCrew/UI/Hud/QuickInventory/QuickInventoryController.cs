using System;
using System.Collections.Generic;
using AloneCrew.Model;
using AloneCrew.Model.Data;
using AloneCrew.UI.Widgets;
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
        
        private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;

        public void Start()
        {
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(_prefab, _container);
            _session = FindFirstObjectByType<GameSession>();
            _trash.Retain(_session.QuickInventoryModel.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = _session.QuickInventoryModel.Inventory;
            _dataGroup.SetData(inventory);
        }

        public void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}