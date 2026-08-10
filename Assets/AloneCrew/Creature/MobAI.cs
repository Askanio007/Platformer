using System.Collections;
using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        
        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1.5f;

        private Coroutine _current;
        private GameObject _target;
        private Creature _creature;
        private bool _isDead;

        private SpawnListComponent _particles;
        private Animator _animator;

        private void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
            StartState(Patrolling());
        }

        public void OnHeroInVision(GameObject go)
        {
            _target = go;
            StartState(AgroToHero());
        }
        

        private IEnumerator Patrolling()
        {
            yield return null;
        }
        
        private IEnumerator AgroToHero()
        {
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(GoToHero());
            
        }
        
        private IEnumerator GoToHero()
        {
            while (_vision.IsTouchingLayer)
            {
                if (_canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }
            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);

        }

        private void StartState(IEnumerator coroutine)
        {
            _creature.SetDirection(Vector2.zero);
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