using System.Linq;
using UnityEngine;

namespace PixelCrew.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = Object.FindObjectsByType<Canvas>(FindObjectsSortMode.None)
                .FirstOrDefault(c => c.gameObject.layer == LayerMask.NameToLayer("UI") );
            Object.Instantiate(window, canvas.transform);
        }
    }
}