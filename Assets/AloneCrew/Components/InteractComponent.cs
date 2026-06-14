using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    public class InteractComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteract;
        
        public void Interact()
        {
            _onInteract?.Invoke();
        }
    }
}