using System;
using UnityEngine;

public class PlatformMoveComponent : MonoBehaviour
{
    
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float maxDistance = 5f;
    [SerializeField] public bool vertical = false;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }
    void FixedUpdate()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, maxDistance);
        transform.position = vertical
            ? new Vector3(_startPosition.x, _startPosition.y + offset, _startPosition.z)
            : new Vector3(_startPosition.x + offset, _startPosition.y, _startPosition.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
