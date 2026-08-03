using UnityEngine;

namespace AloneCrew
{
    public class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float _speed;
        
        protected Rigidbody2D _rigidbody;
        protected int _direction;

        public virtual void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
        }

        public virtual void FixedUpdate()
        {
            
        }
        
    }
}