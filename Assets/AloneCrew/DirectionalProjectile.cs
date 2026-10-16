using Unity.Mathematics;
using UnityEngine;

namespace AloneCrew
{
    public class DirectionalProjectile : BaseProjectile
    {

        public void Launch(Vector2 direction)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
        }
    }
}