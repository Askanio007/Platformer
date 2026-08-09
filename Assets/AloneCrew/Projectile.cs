using System;
using UnityEngine;

namespace AloneCrew
{
    public class Projectile : BaseProjectile
    {
        
        public override void FixedUpdate()
        {
            var position = _rigidbody.position;
            position.x += _speed * _direction;
            _rigidbody.MovePosition(position);
        }
      
    }
}