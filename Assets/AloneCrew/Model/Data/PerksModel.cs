using System;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils.Disposables;
using TMPro;

namespace AloneCrew.Model.Data
{
    public class PerksModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelection = new StringProperty("double-jump");
        
        private readonly CompositeDisposable _trash = new  CompositeDisposable();
        
        public event Action OnChanged;

        public bool IsDoubleJumpSupported => _data.Perks.Used.Value == "double-jump";
        public bool IsShieldSupported => _data.Perks.Used.Value == "shield";
        
        public PerksModel(PlayerData playerData)
        {
            _data = playerData;
            InterfaceSelection.Value = DefsFacade.I.Perks.All[0].Id;
            
            _trash.Retain(_data.Perks.Used.Subscribe((x, y) => OnChanged?.Invoke()));
            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public string Selected => _data.Perks.Used.Value;
        
        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }


        public bool IsUnlocked(string perkId)
        {
            return _data.Perks.IsUnlocked(perkId);
        }

        public void Unlock(string id)
        {
            var def = DefsFacade.I.Perks.Get(id);
            var IsEnoughResources = _data.Inventory.IsEnough(def.Price);

            if (IsEnoughResources)
            {
                _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
                _data.Perks.AddPerk(id);
            }
            
            OnChanged?.Invoke();
        }

        public void Use(string id)
        {
            _data.Perks.Used.Value = id;
        }
        
        public bool CanBuy(string id)
        {
            var def = DefsFacade.I.Perks.Get(id);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }

        public bool IsUsed(string perkId)
        {
            return _data.Perks.Used.Value == perkId;
        }
    }
}