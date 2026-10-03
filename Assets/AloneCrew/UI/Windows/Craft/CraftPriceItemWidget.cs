using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Windows.Perks
{
    public class CraftPriceItemWidget: MonoBehaviour, IItemRenderer<ItemWithCount>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _needCount;
        [SerializeField] private TextMeshProUGUI _haveCount;
        
        private GameSession _session;
        private ItemWithCount _data;
        
        private void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
            UpdateView();
        }

        public void SetData(ItemWithCount data, int i)
        {
            _data = data;
            if (_session != null)
            {
                UpdateView();
            }
        }

        private void UpdateView()
        {
            var item = DefsFacade.I.Items.Get(_data.ItemId);
            _icon.sprite = item.Icon;
            _needCount.text = _data.Count.ToString();
            _haveCount.text = _session.CountInInventory(_data.ItemId).ToString();
        }

    }
}