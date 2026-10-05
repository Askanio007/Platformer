using AloneCrew.Model;
using AloneCrew.UI.LevelsLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew
{
    public class ReloadScene : MonoBehaviour
    {
        public void ReloadLevel()
        {
            var scene = SceneManager.GetActiveScene();
            var gameSession = FindAnyObjectByType<GameSession>();
            var loader = FindFirstObjectByType<LevelLoader>();
            gameSession.Reset();
            loader.LoadLevel(scene.name);
        }
    }
}

