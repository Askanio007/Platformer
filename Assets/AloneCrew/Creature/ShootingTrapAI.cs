using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public class ShootingTrapAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck _vision;
        
        [SerializeField] private Cooldown _meleeCooldown;
        [SerializeField] private CheckCircleOverlap _meleeAttack;
        [SerializeField] private LayerCheck _meleeCanAttack;
        
        [SerializeField] private Cooldown _rangeCooldown;
        [SerializeField] private SpawnComponent _rangeAttack;
        
        private Animator _animator;

        private void Start()
        {
            _animator = this.GetComponent<Animator>();
        }


        private void Update()
        {
            if (_vision.IsTouchingLayer)
            {
                if (_rangeCooldown.IsReady())
                {
                    _animator.SetTrigger("attack");
                    _rangeCooldown.Reset();
                }
            }
        }

        public void RangeAttack()
        {
            _rangeAttack.Spawn();
        }

    }
}