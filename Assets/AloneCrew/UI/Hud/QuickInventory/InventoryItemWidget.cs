using System;
using AloneCrew.Model;
using AloneCrew.Model.Data;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils.Disposables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _selction;
        [SerializeField] private TextMeshProUGUI _value;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        
        private int _index;

        private void Start()
        {
            var session = FindAnyObjectByType<GameSession>();
            _trash.Retain(session.QuickInventoryModel.SelectedIndex.SubscribeAndInvoke(OnIndexChanged));
        }

        public void OnIndexChanged(int newValue, int _)
        {
            _selction.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
            var def = DefsFacade.I.Items.Get(item.Id);
            _icon.sprite = def.Icon;
            _value.text = def.HasTag(ItemTag.Stackable) ? item.Value.ToString() : string.Empty;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}