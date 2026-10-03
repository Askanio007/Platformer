using AloneCrew.UI.LevelsLoader;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew.Components
{
    public class LoadLevelComponent : MonoBehaviour
    {
        [SerializeField] public string level;
        
        public void LoadLevel()
        {
            var loader = FindFirstObjectByType<LevelLoader>();
            loader.LoadLevel(level);
        }
        
    }
}