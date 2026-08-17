using System.Collections;
using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public class MobAI : AbstractMobAI
    {
        private PatrollingCreature _creature;

        protected override void Awake()
        {
            base.Awake();
            _creature = GetComponent<PatrollingCreature>();
        }

        protected override Creature GetCreature()
        {
            return _creature;
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
            while (true)
            {
                _creature.DoPatroling();
                yield return null;
            }
        }

        private IEnumerator MissHero()
        {
            yield return new WaitForSeconds(_alarmDelay);
            StartState(Patrolling());
        }
        
        private IEnumerator AgroToHero()
        {
            _particles.Spawn("Exclamation");
            GetCreature().SetDirection(Vector2.zero);
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
                if (!_creature.GroundForwardExist())
                {
                    GetCreature().SetDirection(Vector2.zero);
                }
                else if (!_canAttack.IsTouchingLayer)
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }
            StartState(MissHero());
        }

        private IEnumerator Attack()
        {
            while (_canAttack.IsTouchingLayer)
            {
                GetCreature().SetDirection(Vector2.zero);
                _creature.Attack();
                yield return new WaitForSeconds(_attackCooldown);
            }
            StartState(GoToHero());
        }
    }
}