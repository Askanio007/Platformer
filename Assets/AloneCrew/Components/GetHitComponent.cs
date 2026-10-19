using System;
using System.Collections;
using UnityEngine;

namespace AloneCrew.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class GetHitComponent : MonoBehaviour
    {
        [SerializeField] private Material _hitMaterial;
        [SerializeField] private float _hitTime = 0.08f;
        
        private SpriteRenderer _spriteRenderer;
        private Material _defaultMaterial;
        private Coroutine _hitCoroutine;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultMaterial = _spriteRenderer.material;
        }

        public void GetHit()
        {
            if (_hitCoroutine != null)
            {
                StopCoroutine(_hitCoroutine);
            }
            _hitCoroutine = StartCoroutine(ProcessHit());
        }

        private IEnumerator ProcessHit()
        {
            _spriteRenderer.material = _hitMaterial;
            yield return new WaitForSeconds(_hitTime);
            _spriteRenderer.material = _defaultMaterial;
        }
    }
}