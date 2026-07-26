using System;
using UnityEngine;

namespace AloneCrew
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private Rigidbody2D _rigidbody;
        private int _direction;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = transform.lossyScale.x > 0 ? 1 : -1;
        }
        
        public void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _speed * _direction;
            _rigidbody.MovePosition(position);
        }
    }
}