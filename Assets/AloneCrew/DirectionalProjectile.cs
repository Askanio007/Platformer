using AloneCrew.Components;
using Unity.Mathematics;
using UnityEngine;

namespace AloneCrew
{
    public class DirectionalProjectile : BaseProjectile
    {
        [SerializeField] private PlaySoundComponent _playSoundComponent;

        public void Launch(Vector2 direction)
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playSoundComponent?.Play("shoot");
            _rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
        }
    }
}