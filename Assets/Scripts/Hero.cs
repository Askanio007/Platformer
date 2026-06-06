using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    private Vector2 _direction;

    [SerializeField] private float _speed;

    void Start()
    {
        
    }

    void Update()
    {
        var delta = _direction * _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + delta.x, transform.position.y + delta.y, transform.position.z);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    
}
