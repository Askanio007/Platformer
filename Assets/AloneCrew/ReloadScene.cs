using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew
{
    public class ReloadScene : MonoBehaviour
    {
        public void ReloadLevel()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}

