using System;
using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using AloneCrew.Utils;
using AloneCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Hud.Perks
{
    public class ActivePerkWidget : MonoBehaviour
    {

        [SerializeField] private GameObject _container;
        [SerializeField] private Image _activePerk;
        [SerializeField] private Image _cooldown;

        private GameSession _session;
        private float _timeCooldown;
        private float _leftCooldown;
        private readonly CompositeDisposable _trash = new  CompositeDisposable();
        private readonly CompositeDisposable _coolDownTrash = new  CompositeDisposable();
        public void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
            _trash.Retain(_session.Data.Perks.Used.Subscribe((x, y) => UpdateView()));
            UpdateView();
        }
        
        void Update()
        {
            if (_leftCooldown > 0f)
            {
                _leftCooldown -= Time.deltaTime;
                _cooldown.fillAmount = Mathf.Clamp01(_leftCooldown / _timeCooldown);
            }
        }
        
        void UpdateCooldown(float value)
        {
            _timeCooldown = value;
            _leftCooldown = _timeCooldown;
        }

        private void UpdateView()
        {
            var perkSelected = !string.IsNullOrEmpty(_session.PerksModel.Selected);
            _container.SetActive(perkSelected);
            _coolDownTrash.Dispose();
            if (perkSelected)
            {
                var perk = DefsFacade.I.Perks.Get(_session.PerksModel.Selected);
                _activePerk.sprite = perk.Icon;
                _cooldown.sprite = perk.Icon;
                var dispose = _session.PerksModel.SubscribeOnCooldown(UpdateCooldown);
                if (dispose != null)
                {
                    _coolDownTrash.Retain(dispose);
                }
            }
        }

        public void OnDestroy()
        {
            _trash.Dispose();
            _coolDownTrash.Dispose();
        }
    }
}