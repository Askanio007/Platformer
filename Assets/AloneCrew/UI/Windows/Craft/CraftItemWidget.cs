using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.Model.Definitions.Repositories;
using AloneCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Windows.Perks
{
    public class CraftItemWidget : MonoBehaviour, IItemRenderer<CraftableDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private GameObject _isSelected;
        
        private GameSession _session;
        private CraftableDef _data;
        
        private void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
            UpdateView();
        }

        public void SetData(CraftableDef data, int i)
        {
            _data = data;
            if (_session != null)
            {
                UpdateView();
            }
        }

        private void UpdateView()
        {
            var item = DefsFacade.I.Items.Get(_data.Id);
            _icon.sprite = item.Icon;
            _name.text = item.Id;
            _isSelected.SetActive(_session.CraftableModel.InterfaceSelection.Value == _data.Id);
            
        }

        public void OnSelect()
        {
            _session.CraftableModel.InterfaceSelection.Value = _data.Id;
        }
    }
}