using System;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHealth;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private IntChange _onChange;
        
        public int Health => _health;
        public int MaxHealth => _maxHealth;

        public void SetHealth(int health)
        {
            _health = health;
        }

        public void ApplyDamage(int damage)
        {
            _health = Math.Min(_maxHealth, _health - damage);
            _onChange?.Invoke(_health);
            if (damage < 0)
            {
                _onHealth?.Invoke();
            }
            else if (damage > 0)
            {
                _onDamage?.Invoke();
            }
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
        
        public void ApplyHealth(int health)
        {
            ApplyDamage(health * (-1));
        }
        
        
    }
}


