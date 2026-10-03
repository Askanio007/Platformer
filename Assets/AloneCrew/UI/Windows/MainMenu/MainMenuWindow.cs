using System;
using AloneCrew.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using AloneCrew.UI.LevelsLoader;

namespace AloneCrew.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        
        private Action _closeAction;
        public void OnShowSetting()
        {
            WindowUtils.CreateWindow("UI/SettingWindow");
        }
        
        public void OnShowLanguage()
        {
            WindowUtils.CreateWindow("UI/LanguageWindow");
        }

        public void OnStartGame()
        {
            var loader = FindFirstObjectByType<LevelLoader>();
            _closeAction = () => { loader.LoadLevel("Level1"); };
            Hide();
        }

        public void OnExit()
        {
            _closeAction = () =>
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Hide();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }
        
    }
}