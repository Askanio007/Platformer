using System;
using System.Linq;
using AloneCrew.Model.Data.Properties;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils.Disposables;

namespace AloneCrew.Model.Data
{
    public class StatsModel : IDisposable
    {
        private readonly PlayerData _data;
        public event Action OnChanged;
        public event Action<StatId> OnUpgrade;
        
        public StatIdProperty InterfaceSelectedStat = new StatIdProperty(StatId.Hp) ; 
        
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public StatsModel(PlayerData data)
        {
            _data = data;
            _trash.Retain(InterfaceSelectedStat.Subscribe((x,y) => OnChanged?.Invoke()));
        }

        public void LevelUp(StatId statId)
        {
            var def = DefsFacade.I.Player.GetStat(statId);
            var nextLevel = GetLevel(statId) + 1;
            if (def.Levels.Length < nextLevel) return;
            
            var price = def.Levels[nextLevel].Price;
            if (!_data.Inventory.IsEnough(price)) return;
            
            _data.Inventory.Remove(price.ItemId, price.Count);
            _data.Levels.LevelUp(statId);
            
            OnUpgrade?.Invoke(statId);
            
            OnChanged?.Invoke();

        }

        public float GetCurrentValue(StatId statId, int level = -1)
        {
            return GetCurrentLevelDef(statId, level).Value;
        }
        
        public float GetValue(StatId statId)
        {
            return GetLevelDef(statId).Value;
        }

        public StatLevel GetCurrentLevelDef(StatId statId, int level = -1)
        {
            if (level == -1) level = GetLevel(statId);
            var def = DefsFacade.I.Player.GetStat(statId);
            return level >= def.Levels.Length ? default : def.Levels[level];
        }
        
        public StatLevel GetLevelDef(StatId statId)
        {
            var def = DefsFacade.I.Player.GetStat(statId);
            return def.Levels[GetLevel(statId)];
        }

        public int GetLevel(StatId statId)
        {
            return _data.Levels.GetLevel(statId);
        }
        
        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }
        
        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}