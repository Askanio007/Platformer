using UnityEngine;
using UnityEngine.InputSystem;

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
    }
}


