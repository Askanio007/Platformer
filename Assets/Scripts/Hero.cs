using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private Vector3 _groundCheckPositionDelta;

    private Vector2 _direction;
    private Rigidbody2D _rigedbody;
    private Color _gizmoColor;


    void Awake()
    {
        _rigedbody = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }

    void FixedUpdate()
    {
        _rigedbody.linearVelocity = new Vector2(_direction.x * _speed, _rigedbody.linearVelocity.y);
        bool isGrounded = IsGrounded();
        if (_direction.y > 0)
        {
            if (isGrounded)
            {
                _rigedbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            }
        } 
        else if (_rigedbody.linearVelocity.y > 0)
        {
            _rigedbody.linearVelocity = new Vector2(_rigedbody.linearVelocity.x, _rigedbody.linearVelocity.y * 0.5f);
        }
        
    }

    private bool IsGrounded()
    {
        bool existHit = Physics2D.OverlapCircle(transform.position + _groundCheckPositionDelta, _groundCheckRadius, _groundLayer);
        _gizmoColor = existHit ? Color.green : Color.red;
        return existHit;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawSphere(transform.position + _groundCheckPositionDelta, _groundCheckRadius);
    }

}
