using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.UI.Widgets;
using AloneCrew.Utils.Disposables;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Windows.PlayerStats
{
    public class PlayerStatsWindow : AnimatedWindow
    {
        [SerializeField] private Transform _statsContainer;
        [SerializeField] private StatWidget _prefab;

        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private ItemWidget _price;
        
        private DataGroup<StatDef, StatWidget> _statsGroup;

        private GameSession _session;
        private readonly CompositeDisposable _trash =  new CompositeDisposable();

        protected override void Start()
        {
            base.Start();
            
            _statsGroup = new DataGroup<StatDef, StatWidget>(_prefab, _statsContainer);
            
            _session.StatsModel.InterfaceSelectedStat.Value = DefsFacade.I.Player.Stats[0].ID;
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.StatsModel.Subscribe(OnStatChanged));
            _trash.Retain(_upgradeButton.onClick.Subscribe(OnUpgrade));
                
            OnStatChanged();
        }

        public void OnUpgrade()
        {
            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            _session.StatsModel.LevelUp(selected);
        }

        private void OnStatChanged()
        {
            var stats = DefsFacade.I.Player.Stats;
            _statsGroup.SetData(stats);
            
            var selected = _session.StatsModel.InterfaceSelectedStat.Value;
            var nextLevel = (int) _session.StatsModel.GetCurrentValue(selected) + 1;
            var def = _session.StatsModel.GetCurrentLevelDef(selected, nextLevel);
            _price.SetData(def.Price);

            var priceExist = def.Price.Count != 0;
            _price.gameObject.SetActive(priceExist);
            _upgradeButton.gameObject.SetActive(priceExist);
            
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}