using AloneCrew.Components;
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

        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private Vector3 _groundCheckPositionDelta;
        
        [SerializeField] private SpawnComponent _footDust;
        [SerializeField] private SpawnComponent _jumpDust;
        [SerializeField] private SpawnComponent _fallDust;
        [SerializeField] private ParticleSystem _hitParticles;
        

        private Vector2 _direction;
        private Rigidbody2D _rigedbody;
        private Color _gizmoColor;
        private Animator _animator;
        private static readonly int isGroundedKey = Animator.StringToHash("is-grounded");
        private static readonly int isRunningKey = Animator.StringToHash("is-running");
        private static readonly int verticalVelocityKey = Animator.StringToHash("vertical-velocity");
        private static readonly int hitKey = Animator.StringToHash("hit");
        private static readonly int healthKey = Animator.StringToHash("health");

        private bool _isGrounded;
        private bool _allowDoubleJump;
        private bool _needFallDust;
        private int _coins;

        void Awake()
        {
            _rigedbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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
            _coins += value;
            Debug.Log($"Get {value}; All coins={_coins}");
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
            if (_coins <= 0)
            {
                return;
            }

            var numberCoinsToDispose = Mathf.Min(_coins, 5);
            _coins -= numberCoinsToDispose;

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
            bool existHit = Physics2D.OverlapCircle(transform.position + _groundCheckPositionDelta, _groundCheckRadius,
                _groundLayer);
            _gizmoColor = existHit ? Color.green : Color.red;
            return existHit;
        }
        
        public void Interact()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactCheckRadius, _interactLayer);
            collider?.gameObject.GetComponent<InteractComponent>()?.Interact();
        }

        public void SpawnDust()
        {
            _footDust.Spawn();
        }
        
        public void SpawnJumpDust()
        {
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
        }
    }
}