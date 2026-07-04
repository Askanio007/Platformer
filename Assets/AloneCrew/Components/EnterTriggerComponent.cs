using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _event;
        [SerializeField] private TriggerEnterObjectEvent _eventCollider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _eventCollider?.Invoke(collision);
            if (collision.gameObject.CompareTag(_tag))
            {
                _event.Invoke();
            }
        }

        [Serializable]
        public class TriggerEnterObjectEvent : UnityEvent<Collider2D>
        {
            
        }

    }
}

