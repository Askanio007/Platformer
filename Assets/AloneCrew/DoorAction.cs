using System;
using UnityEngine;

namespace AloneCrew
{
    public class DoorAction : MonoBehaviour
    {
        private static readonly int openKey = Animator.StringToHash("open");
        [SerializeField] private bool _state;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChangeState()
        {
            _state =  !_state;
            _animator.SetBool(openKey, _state);
        }
        
    }
}