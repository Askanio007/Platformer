using System;
using System.Globalization;
using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.Model.Definitions.Localization;
using AloneCrew.UI.Widgets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Windows.PlayerStats
{
    public class StatWidget : MonoBehaviour, IItemRenderer<StatDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _currentValue;
        [SerializeField] private TextMeshProUGUI _increaseValue;
        [SerializeField] private ProgressBarWidget _progress;
        [SerializeField] private GameObject _selector;
        
        private GameSession _session;
        private StatDef _data;

        protected void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();

        }

        public void SetData(StatDef data, int i)
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
            _name.text = LocalizationManager.I.Localize(_data.Name);
            var sessionStatsModel = _session.StatsModel;
            _currentValue.text = sessionStatsModel.GetValue(_data.ID).ToString(CultureInfo.InvariantCulture);
            var currentLevel = sessionStatsModel.GetLevel(_data.ID);
            var nextLevel = currentLevel + 1;
            var increaseValue = sessionStatsModel.GetCurrentValue(_data.ID, nextLevel);
            _increaseValue.text = $"+ {increaseValue}";
            _increaseValue.gameObject.SetActive(increaseValue > 0);
            
            var levels = DefsFacade.I.Player.GetStat(_data.ID).Levels.Length - 1;
            _progress.SetProgress(currentLevel / (float) levels);
            
            _selector.SetActive(sessionStatsModel.InterfaceSelectedStat.Value == _data.ID);
        }

        public void OnSelect()
        {
            _session.StatsModel.InterfaceSelectedStat.Value = _data.ID;
        }
    }
}