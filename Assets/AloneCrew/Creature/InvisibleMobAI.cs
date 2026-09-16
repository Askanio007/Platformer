using System.Collections;
using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public class InvisibleMobAI : AbstractMobAI
    {
        [SerializeField] private float _attackDelay = 0.05f;
        [SerializeField] private float _invisTimeSec = 1f;
        [SerializeField] private Canvas _healthCanvas;
        private Creature _creature;
        private bool _isAttack;

        protected override void Awake()
        {
            base.Awake();
            _creature = GetComponent<Creature>();
        }
        
        protected override Creature GetCreature()
        {
            return _creature;
        }

        public void OnHeroInVision(GameObject go)
        {
            _target = go;
            if (!_isAttack)
            {
                StartState(AgroToHero());
            }
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
                transform.gameObject.layer = LayerMask.NameToLayer("Trash");
                _isAttack = true;
                _healthCanvas.enabled = false;
                _animator.SetBool("is-invis", true);
                yield return new WaitForSeconds(_invisTimeSec);
                var backPoint = _target.GetComponent<BackPointComponent>();
                if (backPoint != null)
                {
                    transform.position = backPoint.Transform.position;
                    SetDirectionToTarget();
                    StartState(Attack());
                }
                _animator.SetBool("is-invis", false);
                _healthCanvas.enabled = true;
                transform.gameObject.layer = LayerMask.NameToLayer("Enemy");
            }
        }

        private IEnumerator Attack()
        {
            Debug.Log("Attack!");
            yield return new WaitForSeconds(_attackDelay);
            _creature.Attack(); 
            yield return new WaitForSeconds(_attackCooldown);
            _isAttack = false;
            StartState(DoInvisible());
        }
    }
}