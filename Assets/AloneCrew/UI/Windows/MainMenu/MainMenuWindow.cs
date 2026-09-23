using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew.UI.MainMenu
{
    public class MainMenuWindow : AnimatedWindow
    {
        
        private Action _closeAction;
        public void OnShowSetting()
        {
            Show("UI/SettingWindow");
        }
        
        public void OnShowLanguage()
        {
            Show("UI/LanguageWindow");
        }
        
        private void Show(string path)
        {
            var window = Resources.Load<GameObject>(path);
            var canvas = FindFirstObjectByType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnStartGame()
        {
            _closeAction = () => { SceneManager.LoadScene("Level1"); };
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