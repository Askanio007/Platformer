using System.Collections;
using AloneCrew.Components;
using AloneCrew.Model;
using AloneCrew.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace AloneCrew
{
    public class Hero : Creature
    {
        [SerializeField] private LayerMask _interactLayer;
        [SerializeField] private float _interactCheckRadius;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private ParticleSystem _hitParticles;
        
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        
        private Color _handlesColor;
        private bool _allowDoubleJump;
        
        private GameSession _gameSession;
        
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
            _gameSession.Data.SwordCount = 5;
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


        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_isGrounded)
            {
                _allowDoubleJump = true;
            }
        }

        protected override bool NeedJumpDust()
        {
            return !_allowDoubleJump;
        }


        public void AddCoin(int value)
        {
            _gameSession.Data.Coins += value;
            Debug.Log($"Get {value}; All coins={_gameSession.Data.Coins}");
        }
        
        public void AddSword(int value)
        {
            _gameSession.Data.SwordCount += value;
            Debug.Log($"Get {value}; All swords={_gameSession.Data.SwordCount}");
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            SpawnCoins();
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

        protected override float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigedbody.linearVelocityY <= 0.001f;
            if (!isFalling) return yVelocity;
            
            yVelocity = base.CalculateJumpVelocity(yVelocity);
            if (!_isGrounded && _allowDoubleJump)
            {
                yVelocity = _jumpSpeed;
                _allowDoubleJump = false;
                _particles.Spawn("jump");
            }
            return yVelocity;
        }


        public void Interact()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _interactCheckRadius, _interactLayer);
            collider?.gameObject.GetComponent<InteractComponent>()?.Interact();
        }
        
        public override void Attack()
        {
            if (!_gameSession.Data.IsArmed) return;
            base.Attack();
        }
        
        public override void Throw()
        {
            if (!_gameSession.Data.IsArmed) return;
            if (_throwCooldown.IsReady())
            {
                if (TryThrow())
                {
                    _throwCooldown.Reset();
                }
            }
        }
        
        public void BigThrow()
        {
            if (!_gameSession.Data.IsArmed) return;
            StartCoroutine(ThrowBatch());
        }

        private IEnumerator ThrowBatch()
        {
            for (int i = 0; i < 3; i++)
            {
                
                TryThrow();
                yield return new WaitForSeconds(0.1f);
            }
        }

        private bool TryThrow()
        {
            var swordCount = _gameSession.Data.SwordCount;
            if (swordCount > 1)
            {
                base.Throw();
                _gameSession.Data.SwordCount = swordCount-1;
                return true;
            }
            return false;
        }
        
        
    }
    
    
}