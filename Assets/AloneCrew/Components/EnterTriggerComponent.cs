using System;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer = ~0;
        [SerializeField] private UnityEvent _event;
        [SerializeField] private TriggerEnterObjectEvent _eventCollider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((_layer.value & (1 << collision.gameObject.layer)) == 0) return;
            if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;
            _event?.Invoke();
            _eventCollider?.Invoke(collision.gameObject);
        }

        [Serializable]
        public class TriggerEnterObjectEvent : UnityEvent<GameObject>
        {
            
        }

    }
}

