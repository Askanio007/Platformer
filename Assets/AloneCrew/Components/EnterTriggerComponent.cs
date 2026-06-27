using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _event;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tag))
            {
                _event.Invoke();
            }
        }

    }
}

