using System.Collections;
using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public class InvisibleMobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        [SerializeField] private LayerCheck _canAttack;
        
        [SerializeField] private float _alarmDelay = 0.5f;
        [SerializeField] private float _attackCooldown = 1.5f;
        [SerializeField] private float _attackDelay = 0.05f;

        private Coroutine _current;
        private GameObject _target;
        private Creature _creature;
        private bool _isDead;
        private bool _isAttack;
        private Animator _animator;

        private void Awake()
        {
            _creature = GetComponent<Creature>();
            _animator = GetComponent<Animator>();
        }
        
        private void Start()
        {
        }

        public void OnHeroInVision(GameObject go)
        {
            _target = go;
            if (!_isAttack)
            {
                StartState(AgroToHero());
            }
        }

        private IEnumerator MissHero()
        {
            yield return new WaitForSeconds(_alarmDelay);
        }
        
        private IEnumerator AgroToHero()
        {
            SetDirectionToTarget();
            yield return new WaitForSeconds(_alarmDelay);
            StartState(DoInvisible());
            
        }
        
        
        
        private IEnumerator DoInvisible()
        {
            if (_vision.IsTouchingLayer)
            {
                _isAttack = true;
                _animator.SetBool("is-invis", true);
                yield return new WaitForSeconds(2f);
                var backPoint = _target.GetComponent<BackPointComponent>();
                if (backPoint != null)
                {
                    transform.position = backPoint.Transform.position;
                    SetDirectionToTarget();
                    StartState(Attack());
                }
                _animator.SetBool("is-invis", false);
            }
        }

        private IEnumerator Attack()
        {
            Debug.Log("Attack!");
            yield return new WaitForSeconds(_attackDelay);
            _creature.Attack(); 
            _isAttack = false;
            yield return new WaitForSeconds(_attackCooldown);
            StartState(DoInvisible());
        }

        private void SetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            _creature.SetDirection(direction.normalized);

        }

        private void StartState(IEnumerator coroutine)
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