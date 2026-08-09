using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace AloneCrew
{
    public class HeroInputReader : MonoBehaviour
    {
        private Hero _hero;
        void Awake()
        {
            _hero =  GetComponent<Hero>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnSaySometihing(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                Debug.Log("Say");
            }

        }
        
        public void OnInteract(InputAction.CallbackContext context)
        {
            if(context.canceled)
            {
                _hero.Interact();
            }
        }
        
        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.canceled)
            {
                _hero.Attack();
            }
        }
        
        public void OnArmed(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                _hero.UpdateArm();
            }
        }
        
        public void OnHeal(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                _hero.Heal();
            }
        }
        
        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.interaction is HoldInteraction && context.performed)
            {
                _hero.BigThrow();
            }
            else if (context.interaction is TapInteraction && (context.started || context.canceled))
            {
                _hero.Throw();
            }
            
        }
    }
}


