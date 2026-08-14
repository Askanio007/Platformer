using System.Collections;
using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public abstract class AbstractMobAI : MonoBehaviour
    {
        [SerializeField] protected LayerCheck _vision;
        [SerializeField] protected LayerCheck _canAttack;
        
        [SerializeField] protected float _alarmDelay = 0.5f;
        [SerializeField] protected float _attackCooldown = 1.5f;

        protected Coroutine _current;
        protected GameObject _target;
        protected bool _isDead;

        protected SpawnListComponent _particles;
        protected Animator _animator;
        
        protected abstract Creature GetCreature();

        protected virtual void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _animator = GetComponent<Animator>();
        }

        protected void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            GetCreature().SetDirection(direction.normalized);
        }

        protected void StartState(IEnumerator coroutine)
        {
            if (_isDead) return;
            if (_current != null)
            {
                StopCoroutine(_current);
            }
            _current = StartCoroutine(coroutine);
        }
        
        public void OnDie()
        {
            _isDead = true;
            _animator.SetTrigger("is-dead");
            if (_current != null)
                StopCoroutine(_current);
        }
        
    }
}