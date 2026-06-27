using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _event;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                _event?.Invoke(other.gameObject);
            }
        
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
            
        }
    }
}

