using UnityEngine;

namespace AloneCrew.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger("show");
        }

        public void Hide()
        {
            _animator.SetTrigger("hide");
        }
        
        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
        
    }
}