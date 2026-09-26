using System;
using AloneCrew.Model;
using AloneCrew.Model.Definitions.Repositories;
using AloneCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Windows.Perks
{
    public class PerkWidget : MonoBehaviour, IItemRenderer<PerkDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isLocked ;
        [SerializeField] private GameObject _isSelected;
        [SerializeField] private GameObject _isUsed;
        
        private GameSession _session;
        private PerkDef _data;

        private void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
            UpdateView();
        }

        public void SetData(PerkDef data, int i)
        {
            _data = data;
            if (_session != null)
            {
                UpdateView();
            }
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isUsed.SetActive(_session.PerksModel.IsUsed(_data.Id));
            _isSelected.SetActive(_session.PerksModel.InterfaceSelection.Value == _data.Id);
            _isLocked.SetActive(!_session.PerksModel.IsUnlocked(_data.Id));
            
        }

        public void OnSelect()
        {
            _session.PerksModel.InterfaceSelection.Value = _data.Id;
        }
    }
}