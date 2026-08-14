using System.Collections;
using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew
{
    public class PatrollingCreature : Creature
    {
        [SerializeField] private LayerCheck _forwardGroundCheck;
        [SerializeField] private Vector2 _patrollingDirection;

        public void DoPatroling()
        {
            if (_direction != Vector2.zero)
            {
                _patrollingDirection = _direction;
            }
            if (!_forwardGroundCheck.IsTouchingLayer)
            {
                _patrollingDirection = _patrollingDirection == Vector2.left ? Vector2.right : Vector2.left;
            }
            SetDirection(_patrollingDirection);
        }

        public bool GroundForwardExist()
        {
            return _forwardGroundCheck.IsTouchingLayer;
        }
        
    }
}