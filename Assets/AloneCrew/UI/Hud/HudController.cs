using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.UI.Widgets;
using UnityEngine;

namespace AloneCrew.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        private GameSession _session;

        public void Start()
        {
            _session = FindAnyObjectByType<GameSession>();
            _session.Data.Hp.OnChanged += OnHealthChanged;
            
            OnHealthChanged(_session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var val = newValue/maxHealth; 
            _healthBar.SetProgress(val);
            
        }

        private void OnDestroy()
        {
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}