using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.Model.Definitions.Repositories;
using AloneCrew.UI.Widgets;
using AloneCrew.Utils.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Windows.Perks
{
    public class CraftWindow : AnimatedWindow
    {
        [SerializeField] private Button _craftButton;
        [SerializeField] private Image _selectedIcon;
        [SerializeField] private Transform _listCraftableContainer;
        [SerializeField] private Transform _listCraftablePriceContainer;
        [SerializeField] private CraftPriceItemWidget _craftablePricePrefab;
        [SerializeField] private CraftItemWidget _craftableListItemPrefab;
        

        private DataGroup<CraftableDef, CraftItemWidget> _dataGroup;
        private DataGroup<ItemWithCount, CraftPriceItemWidget> _priceDataGroup;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;

        protected override void Start()
        {
            base.Start();

            _dataGroup = new DataGroup<CraftableDef, CraftItemWidget>(_craftableListItemPrefab, _listCraftableContainer);
            _priceDataGroup = new DataGroup<ItemWithCount, CraftPriceItemWidget>(_craftablePricePrefab, _listCraftablePriceContainer);
            _session = FindAnyObjectByType<GameSession>();
            
            _trash.Retain(_craftButton.onClick.Subscribe(OnCraft));
            _trash.Retain(_session.CraftableModel.Subscribe(OnCraftableChanged));

            OnCraftableChanged();
        }
        
        private void OnCraftableChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Craftables.All);
            
            var selected = _session.CraftableModel.InterfaceSelection.Value;
            _craftButton.interactable = _session.CraftableModel.CanCraft(selected);
            
            var def = DefsFacade.I.Craftables.Get(selected);
            _priceDataGroup.SetData(def.Price);

            var itemDef = DefsFacade.I.Items.Get(selected);
            _selectedIcon.sprite = itemDef.Icon;
        }

        public void OnCraft()
        {
            var selected = _session.CraftableModel.InterfaceSelection.Value;
            _session.CraftableModel.Craft(selected);
        }
        
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}