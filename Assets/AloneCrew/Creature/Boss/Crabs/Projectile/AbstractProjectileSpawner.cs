using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AloneCrew
{
    public abstract class AbstractProjectileSpawner : MonoBehaviour
    {
        [SerializeField] protected GameObject _projectilePrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] ProjectileSpawnerSetting[] _setting;
        
        private event Action OnCompleted;
        private Coroutine _spawnCoroutine;
        public int Stage { get; set; }

        [ContextMenu("Launch")]
        public void LaunchProjectile(Action onCompleted)
        {
            if (_spawnCoroutine != null) return;
            OnCompleted = onCompleted;
            _spawnCoroutine = StartCoroutine(SpawnProjectile());
        }

        public void Stop()
        {
            if (_spawnCoroutine != null) StopCoroutine(_spawnCoroutine);
        }
        
        public virtual IEnumerator SpawnProjectile()
        {
            var spawnPoints = InitQueue();
            var setting = _setting[Stage];
            var allCount = setting.Count;
            while (allCount > 0)
            {
                var point = spawnPoints.Dequeue();
                Launch(point);
                allCount--;
                spawnPoints.Enqueue(point);
                yield return new WaitForSeconds(setting.DelayTime);
            }
            _spawnCoroutine = null;
            OnCompleted?.Invoke();
            OnCompleted = null;
            _spawnCoroutine = null;
        }

        private Queue<Transform> InitQueue()
        {
            var spawnPoints = new Queue<Transform>();
            foreach (var point in _spawnPoints)
            {
                spawnPoints.Enqueue(point);
            }
            return spawnPoints;
        }
        
        protected abstract void Launch(Transform spawnPoint);

        protected void Launch(GameObject projectile, Vector2 direction)
        {
            projectile.GetComponent<DirectionalProjectile>().Launch(direction);
        }
    }

    [Serializable]
    public struct ProjectileSpawnerSetting
    {
        [SerializeField] private int _count;
        [SerializeField] private float _delayTime;

        public int Count => _count;

        public float DelayTime => _delayTime;
    }
    
    
}