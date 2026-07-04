using AloneCrew.Components;
using AloneCrew.Model;
using AloneCrew.Utils;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace AloneCrew
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _interactLayer;
        [SerializeField] private float _interactCheckRadius;
        [SerializeField] private float _longFly;
        [SerializeField] private int _attack;

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;
        [SerializeField] private CheckCircleOverlap _attackRange;
        
        [SerializeField] private SpawnComponent _footDust;
        [SerializeField] private SpawnComponent _jumpDust;
        [SerializeField] private SpawnComponent _fallDust;
        [SerializeField] private ParticleSystem _hitParticles;
        
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        

        private Vector2 _direction;
        private Rigidbody2D _rigedbody;
        private Color _handlesColor;
        private Animator _animator;
        private static readonly int isGroundedKey = Animator.StringToHash("is-grounded");
        private static readonly int isRunningKey = Animator.StringToHash("is-running");
        private static readonly int verticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int hitKey = Animator.StringToHash("hit");
        private static readonly int healthKey = Animator.StringToHash("health");
        private static readonly int attackKey = Animator.StringToHash("attack");

        private bool _isGrounded;
        private bool _allowDoubleJump;
        private bool _needFallDust;
        private GameSession _gameSession;

        void Awake()
        {
            _rigedbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
        
        void Start()
        {
            InitSession();
        }

        public void UpdateArm()
        {
            _gameSession.Data.IsArmed = !_gameSession.Data.IsArmed;
            UpdateAnimator();
        }

        private void InitSession()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
            GetComponent<HealthComponent>().SetHealth(_gameSession.Data.Hp);
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            _animator.runtimeAnimatorController = _gameSession.Data.IsArmed ? _armed : _disarmed;
        }
        

        public void OnHealthChange(int health)
        {
            _gameSession.Data.Hp = health;
        }
        

        void Update()
        {
            var grounded = IsGrounded();
            _isGrounded = grounded;
        }


        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
        
        public void SetJumpSpeed(float speed)
        {
            _jumpSpeed = speed;
        }

        void FixedUpdate()
        {
            if (_isGrounded)
            {
                if (_needFallDust)
                {
                    _fallDust.Spawn();
                    _needFallDust = false;
                }
                _allowDoubleJump = true;
            }
            var xVelocity = _direction.x * _speed;
            var yVelocity = CalculateYVelocity();
            if ((yVelocity < _longFly || !_allowDoubleJump) && !_needFallDust && !_isGrounded)
            {
                _needFallDust = true;
            }
            _rigedbody.linearVelocity = new Vector2(xVelocity, yVelocity);

            _animator.SetBool(isGroundedKey, _isGrounded);
            _animator.SetBool(isRunningKey, _direction.x != 0);
            _animator.SetFloat(verticalVelocityKey, _rigedbody.linearVelocity.y);

            UpdateSpriteDirection();
        }

        public void AddCoin(int value)
        {
            _gameSession.Data.Coins += value;
            Debug.Log($"Get {value}; All coins={_gameSession.Data.Coins}");
        }

        public void TakeDamage()
        {
            _animator.SetTrigger(hitKey);
            _rigedbody.linearVelocity = new Vector2(_rigedbody.linearVelocityX, _damageJumpSpeed);
            
            SpawnCoins();
        }
        
        public void TakeHealth()
        {
            _animator.SetTrigger(healthKey);
        }

        private void SpawnCoins()
        {
            if (_gameSession.Data.Coins <= 0)
            {
                return;
            }

            var numberCoinsToDispose = Mathf.Min(_gameSession.Data.Coins, 5);
            _gameSession.Data.Coins -= numberCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numberCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);
            
            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
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

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigedbody.linearVelocityY <= 0.001f;
            if (!isFalling) return yVelocity;

            if (_isGrounded)
            {
                yVelocity += _jumpSpeed;
                _jumpDust.Spawn();
            }
            else if (_allowDoubleJump)
            {
                yVelocity = _jumpSpeed;
                _allowDoubleJump = false;
                _jumpDust.Spawn();
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                transform.localScale = Vector3.one;
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private bool IsGrounded()
        {
            bool existHit = Physics2D.OverlapCircle(transform.position + _groundCheckPositionDelta, _groundCheckRadius, _groundLayer);
            return existHit;
        }
        
        public void Interact()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactCheckRadius, _interactLayer);
            collider?.gameObject.GetComponent<InteractComponent>()?.Interact();
        }
        
        public void Attack()
        {
            if (!_gameSession.Data.IsArmed) return;
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
            _footDust.Spawn();
        }
        
        public void SpawnJumpDust()
        {
            
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = IsGrounded() ? HandlesUtils.TransparentGreen : HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position + _groundCheckPositionDelta, Vector3.forward, _groundCheckRadius);
        }
#endif
    }
}