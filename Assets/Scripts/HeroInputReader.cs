using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    private Hero _hero;
    void Awake()
    {
        _hero =  GetComponent<Hero>();
    }

    public void OnHorizontalMovement(InputAction.CallbackContext context)
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
}
