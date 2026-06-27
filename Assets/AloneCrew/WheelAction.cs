using System;
using UnityEngine;

namespace AloneCrew
{
    public class WheelAction : MonoBehaviour
    {
        private static readonly int rollKey = Animator.StringToHash("roll");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChangeState()
        {
            _animator.SetTrigger(rollKey);
        }
        
    }
}