using System.Collections.Generic;
using AloneCrew.Utils;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace AloneCrew
{
    public class CheckCapsuleOverlap : ICheckOverlap
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private float _angle;
        [SerializeField] private CapsuleDirection2D _direction;
        [SerializeField] private string _tag;

        public override GameObject[] GetObjectsInRange()
        {
            var colliders = Physics2D.OverlapCapsuleAll(transform.position, _size, _direction, _angle);
            var result = new List<GameObject>();
            foreach (var collider in colliders)
            {
                if (collider.CompareTag(_tag))
                {
                    result.Add(collider.gameObject);
                }
            }
            return result.ToArray();
        }
        
    }
}