using AloneCrew.Model.Definitions;
using AloneCrew.Model.Definitions.Repositories;
using AloneCrew.UI.Windows.Perks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Widgets
{
    public class ItemWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _value;


        public void SetData(ItemWithCount price)
        {
            var def = DefsFacade.I.Items.Get(price.ItemId);
            _icon.sprite = def.Icon;
            _value.text = price.Count.ToString();
        }
    }
}