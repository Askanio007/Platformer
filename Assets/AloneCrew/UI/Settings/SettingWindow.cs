using System;
using AloneCrew.Model.Data;
using AloneCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew.UI.Settings
{
    public class SettingWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;
        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.I.Music);
            _sfx.SetModel(GameSettings.I.Sfx);
        }
    }
}