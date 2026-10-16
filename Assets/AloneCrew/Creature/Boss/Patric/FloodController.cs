using System.Collections;
using AloneCrew.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace AloneCrew.Boss.Patric
{
    public class FloodController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _floodTime;
        private static readonly int floodKey = Animator.StringToHash("IsFlooding");
        private Coroutine _floodCoroutine;

        public void StartFlood()
        {
            if (_floodCoroutine != null) return;
            _floodCoroutine = StartCoroutine(ExecuteFlood());
        }

        private IEnumerator ExecuteFlood()
        {
            _animator.SetBool(floodKey, true);
            yield return new WaitForSeconds(_floodTime);
            _animator.SetBool(floodKey, false);
            _floodCoroutine = null;
        }
    }
}