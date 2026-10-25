using UnityEngine;

namespace AloneCrew.Components
{
    public class ExitComponent : MonoBehaviour
    {
        public void OnExit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}