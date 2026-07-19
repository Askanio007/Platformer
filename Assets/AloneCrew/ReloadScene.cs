using AloneCrew.Model;
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
            gameSession.Reset();
            SceneManager.LoadScene(scene.name);
        }
    }
}

