using System.Collections.Generic;
using AloneCrew.Utils;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace AloneCrew
{
    public class CheckCircleOverlap : ICheckOverlap
    {
        [SerializeField] private float _radius;
        [SerializeField] private string _tag;

        public override GameObject[] GetObjectsInRange()
        { 
            var colliders = Physics2D.OverlapCircleAll(transform.position, _radius);
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

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, _radius);
        }
        
    }
}