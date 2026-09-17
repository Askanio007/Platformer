using System.Collections;
using AloneCrew.Components;
using AloneCrew.Model;
using AloneCrew.Model.Data;
using AloneCrew.Model.Definitions;
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
        [SerializeField] protected SpawnComponent _throwable;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;
        
        private Color _handlesColor;
        private bool _allowDoubleJump;
        private bool _isThrowing;
        
        private GameSession _gameSession;
        private PlaySoundComponent _sounds;
        private HealthComponent _healthComponent;

        void Start()
        {
            _sounds = GetComponent<PlaySoundComponent>();
            _healthComponent = GetComponent<HealthComponent>();
            InitSession();
            _gameSession.SubscribeOnInventoryChanged(OnInventoryChanged);
        }

        private void OnDestroy()
        {
            _gameSession.UnsubscribeOnInventoryChanged(OnInventoryChanged);
        }

        public bool AddInInventory(string id, int value)
        {
           return _gameSession.AddToInventory(id, value);
        }

        public void OnInventoryChanged(string id, int value)
        {
            Debug.Log($"Inventory changed {id}: {value}");
        }

        public void UsePotion()
        {
            var itemId = _gameSession.QuickInvSelectedItemId;
            PotionDef potionDef;
            if (!DefsAdapter.TryGetPotionItem(itemId, out potionDef))
            {
                return;
            }
            var potionCount = _gameSession.CountInInventory(itemId);

            if (potionCount > 0)
            {
                if (potionDef.Tag == PotionItemTag.Health)
                {
                    _healthComponent.ApplyHealth(potionDef.Value);
                }
                else if (potionDef.Tag == PotionItemTag.Speed)
                {
                    UpdateSpeed(potionDef.Value);
                }
                else
                {
                    Debug.LogWarning($"Unknown potion tag={potionDef.Tag}");
                    return;
                }
                _gameSession.RemoveFromInventory(itemId, 1);
            }
            
        }
        
        public void NextItem()
        {
            _gameSession.QuickInventoryModel.SetNextItem();
        }
        
        public void UpdateArm()
        {
            _gameSession.IsArmed = !_gameSession.IsArmed;
            UpdateAnimator();
        }

        private void InitSession()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
            AddInInventory("Sword", 5);
            GetComponent<HealthComponent>().SetHealth(_gameSession.Hp);
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            _animator.runtimeAnimatorController = _gameSession.IsArmed ? _armed : _disarmed;
        }

        public void OnDie()
        {
            _sounds.Play(soundDieKey);
        }
        
        public void OnJump()
        {
            _sounds.Play(soundJumpKey);
        }
        

        public void OnHealthChange(int health)
        {
            _gameSession.Hp = health;
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

        public override void TakeDamage()
        {
            base.TakeDamage();
            SpawnCoins();
            _sounds.Play(soundHurtKey);
        }

        private void SpawnCoins()
        {
            var currentCoins = _gameSession.CountInInventory("Coin");
            if (currentCoins <= 0)
            {
                return;
            }

            
            var numberCoinsToDispose = Mathf.Min(currentCoins, 5);
            _gameSession.RemoveFromInventory("Coin", (int)numberCoinsToDispose);

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
            if (!_gameSession.IsArmed) return;
            base.Attack();
            _sounds.Play(soundMeleeKey);
        }
        
        public override void Throw()
        {
            if (!_gameSession.IsArmed) return;
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
            if (!_gameSession.IsArmed || _isThrowing) return;
            _isThrowing = true;
            StartCoroutine(ThrowBatch());
        }

        private IEnumerator ThrowBatch()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!TryThrow())
                {
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            _isThrowing = false;
        }

        private bool TryThrow()
        {
            var throwId =  _gameSession.QuickInvSelectedItemId;
            ThrowableDef throwItemDef;
            if (!DefsAdapter.TryGetThrowableItem(throwId, out throwItemDef))
            {
                return false;
            }
            var throwableCount = _gameSession.CountInInventory(throwId);
            if (throwableCount > throwItemDef.MinAfterThrow)
            {
                base.Throw();
                _gameSession.RemoveFromInventory(throwId, 1);
                _sounds.Play(soundThrowKey);
                return true;
            }
            return false;
        }
        
        public override void DoThrow()
        {
            ThrowableDef throwItemDef;
            if (DefsAdapter.TryGetThrowableItem(_gameSession.QuickInvSelectedItemId, out throwItemDef))
            {
                _throwable.SetPrefabAndSpawn(throwItemDef.Projectile);
            }
            
            
        }
        
    }
    
    
}