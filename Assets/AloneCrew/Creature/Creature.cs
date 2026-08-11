using AloneCrew.Components;
using AloneCrew.Utils;
using UnityEngine;
using UnityEditor;

namespace AloneCrew
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private int _attack;

        [Header("Params")][SerializeField] private bool _invertScale;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        
        [SerializeField] protected SpawnListComponent _particles;
        [SerializeField] private float _longFly;
        
        protected Vector2 _direction;
        protected Rigidbody2D _rigedbody;
        protected Animator _animator;
        public bool _isGrounded;
        private bool _needFallDust;
        
        private static readonly int isGroundedKey = Animator.StringToHash("is-grounded");
        private static readonly int isRunningKey = Animator.StringToHash("is-running");
        private static readonly int verticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int hitKey = Animator.StringToHash("hit");
        private static readonly int healthKey = Animator.StringToHash("health");
        private static readonly int attackKey = Animator.StringToHash("attack");
        private static readonly int throwKey = Animator.StringToHash("throw");
        
        protected static readonly string soundThrowKey = "range";
        protected static readonly string soundJumpKey = "jump";
        protected static readonly string soundDieKey = "die";
        protected static readonly string soundMeleeKey = "melee";
        protected static readonly string soundHurtKey = "hurt";
        
        protected virtual void Awake()
        {
            _rigedbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
        
        public void SetJumpSpeed(float speed)
        {
            _jumpSpeed = speed;
        }
        
        protected virtual void  Update()
        {
            _isGrounded = IsGrounded();
        }
        
        protected virtual void FixedUpdate()
        {
            if (_isGrounded)
            {
                if (_needFallDust)
                {
                    _particles.Spawn("fall_dust");
                    _needFallDust = false;
                }
                
            }
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            if ((yVelocity < _longFly || NeedJumpDust()) && !_needFallDust && !_isGrounded)
            {
                _needFallDust = true;
            }
            _rigedbody.linearVelocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(isGroundedKey, _isGrounded);
            _animator.SetBool(isRunningKey, _direction.x != 0);
            _animator.SetFloat(verticalVelocityKey, _rigedbody.linearVelocity.y);

            UpdateSpriteDirection();
        }
        
        private float CalculateYVelocity()
        {
            var yVelocity = _rigedbody.linearVelocityY;
            var isJumpPressing = _direction.y > 0;
            if (isJumpPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigedbody.linearVelocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
                _particles.Spawn("jump");
            }

            return yVelocity;
        }
        
        private void UpdateSpriteDirection()
        {
            var multiplier = _invertScale ? -1 : 1;
            if (_direction.x > 0)
            {
                transform.localScale = new Vector3(multiplier, 1, 1);
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
            }
        }

        protected virtual bool NeedJumpDust()
        {
            return false;
        }

        
        private bool IsGrounded()
        {
            return _groundCheck.IsTouchingLayer;
        }
        
        public virtual void TakeDamage()
        {
            _animator.SetTrigger(hitKey);
            _rigedbody.linearVelocity = new Vector2(_rigedbody.linearVelocityX, _damageVelocity);
        }
        
        public virtual void Attack()
        {
            _animator.SetTrigger(attackKey);
        }
        
        public void DoAttack()
        {
            var objects = _attackRange.GetObjectsInRange();
            foreach (var go in objects)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null)
                {
                    hp.ApplyDamage(_attack);
                }
                
            }
        }
        
        public void SpawnDust()
        {
            _particles.Spawn("foot_dust");
        }
        
        public virtual void Throw()
        {
            _animator.SetTrigger(throwKey);
        }
        
        public void DoThrow()
        {
            _particles.Spawn("sword_throw");
        }
        
    }
}