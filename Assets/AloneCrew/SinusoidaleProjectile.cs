using Unity.Mathematics;
using UnityEngine;

namespace AloneCrew
{
    public class SinusoidaleProjectile : BaseProjectile
    {

        [SerializeField] private float _frequency;
        [SerializeField] private float _amplitude;
        private float _originalY;
        private float _time;

        public override void Start()
        {
            base.Start();
            _originalY = _rigidbody.position.y;
        }
        
        
        public override void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _direction * _speed;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            _rigidbody.MovePosition((position));
            _time += Time.fixedDeltaTime;
        }
        
        
    }
}