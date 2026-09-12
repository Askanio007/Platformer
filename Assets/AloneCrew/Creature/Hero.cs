using System.Collections;
using AloneCrew.Components;
using AloneCrew.Interaction;
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
        private const int MIN_SWORD_COUNT = 1;
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
        
        private InventoryItemData SelectedItem => _gameSession.QuickInventoryModel.SelectedItem;
        
        void Start()
        {
            _sounds =  GetComponent<PlaySoundComponent>();
            InitSession();
            _gameSession.Data.Inventory.onInventoryChanged += OnInventoryChanged;
        }

        private void OnDestroy()
        {
            _gameSession.Data.Inventory.onInventoryChanged -= OnInventoryChanged;
        }

        public bool AddInInventory(string id, int value)
        {
           return _gameSession.Data.Inventory.Add(id, value);
        }

        public void OnInventoryChanged(string id, int value)
        {
            Debug.Log($"Inventory changed {id}: {value}");
        }

        public void Heal()
        {
            var item = SelectedItem;
            var healthComponent = GetComponent<RequireItemComponent>();
            if (healthComponent != null)
            {
                healthComponent.Check(item.Id);
            }
        }
        
        public void NextItem()
        {
            _gameSession.QuickInventoryModel.SetNextItem();
        }
        
        public void UseItem()
        {
            
        }
        
        public void UpdateArm()
        {
            _gameSession.Data.IsArmed = !_gameSession.Data.IsArmed;
            UpdateAnimator();
        }

        private void InitSession()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
            AddInInventory("Sword", 5);
            GetComponent<HealthComponent>().SetHealth(_gameSession.Data.Hp.Value);
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            _animator.runtimeAnimatorController = _gameSession.Data.IsArmed ? _armed : _disarmed;
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
            _gameSession.Data.Hp.Value = health;
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
            var currentCoins = _gameSession.Data.Inventory.Count("Coin");
            if (currentCoins <= 0)
            {
                return;
            }

            
            var numberCoinsToDispose = Mathf.Min(currentCoins, 5);
            _gameSession.Data.Inventory.Remove("Coin", numberCoinsToDispose);

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
            _sounds.Play(soundMeleeKey);
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
            if (!_gameSession.Data.IsArmed || _isThrowing) return;
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
            var throwId =  _gameSession.QuickInventoryModel.SelectedItem.Id;
            var swordCount = _gameSession.Data.Inventory.Count(throwId);
            if (swordCount > MIN_SWORD_COUNT)
            {
                base.Throw();
                _gameSession.Data.Inventory.Remove(throwId, 1);
                _sounds.Play(soundThrowKey);
                return true;
            }
            return false;
        }
        
        public override void DoThrow()
        {
            var id = _gameSession.QuickInventoryModel.SelectedItem.Id;
            _throwable.SetPrefabAndSpawn(DefsFacade.I.ThrowableItems.Get(id).Projectile);
        }
        
        
        
        
        
    }
    
    
}