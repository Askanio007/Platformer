using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    [RequireComponent(typeof(Animator))]
    public class TriggerAnimationComponent : MonoBehaviour
    {
        [SerializeField] private string _triggerName;
        [SerializeField] private UnityEvent _onEvent;
        private int _triggerKey;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _triggerKey = Animator.StringToHash(_triggerName);
        }

        public void SetTrigger()
        {
            _animator.SetTrigger(_triggerKey);
            _onEvent?.Invoke();
        }
        
    }
}