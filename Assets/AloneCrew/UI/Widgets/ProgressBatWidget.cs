using UnityEngine;
using UnityEngine.UI;

namespace AloneCrew.UI.Widgets
{
    public class ProgressBatWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float progress)
        {
            _bar.fillAmount = progress;
        }
        
    }
}