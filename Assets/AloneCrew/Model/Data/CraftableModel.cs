using System;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils.Disposables;
using TMPro;

namespace AloneCrew.Model.Data
{
    public class CraftableModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelection = new StringProperty("health-potion");
        private readonly CompositeDisposable _trash = new  CompositeDisposable();
        public event Action OnChanged;
        
        public CraftableModel(PlayerData playerData)
        {
            _data = playerData;
            InterfaceSelection.Value = DefsFacade.I.Craftables.All[0].Id;
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }
        
        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Craft(string id)
        {
            var def = DefsFacade.I.Craftables.Get(id);
            var IsEnoughResources = _data.Inventory.IsEnough(def.Price);

            if (IsEnoughResources)
            {
                foreach (var price in def.Price)
                {
                    _data.Inventory.Remove(price.ItemId, price.Count);
                }
                _data.Inventory.Add(id, 1);
            }
            
            OnChanged?.Invoke();
        }
        
        public bool CanCraft(string id)
        {
            var def = DefsFacade.I.Craftables.Get(id);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}