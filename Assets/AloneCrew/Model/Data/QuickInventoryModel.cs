using System;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils.Disposables;
using NUnit.Framework.Internal.Filters;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private readonly PlayerData _playerData;

        public InventoryItemData[] Inventory {get; private set;}
        public readonly IntPersistentProperty SelectedIndex = new IntPersistentProperty(0);
        public event Action OnChanged;
        public InventoryItemData SelectedItem => Inventory[SelectedIndex.Value];
        public QuickInventoryModel(PlayerData playerData)
        {
            _playerData = playerData;

            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable);
            _playerData.Inventory.onInventoryChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action onChanged)
        {
            OnChanged += onChanged;
            return new ActionDisposable(() => OnChanged -= onChanged);
            
        }

        private void OnChangedInventory(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, item => item.Id == id);
            Inventory = _playerData.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }
        
        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public void Dispose()
        {
            _playerData.Inventory.onInventoryChanged -= OnChangedInventory;
        }
    }
}