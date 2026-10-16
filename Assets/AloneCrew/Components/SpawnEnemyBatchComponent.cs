using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

namespace AloneCrew.Components
{
    public class SpawnEnemyBatchComponent : MonoBehaviour
    {
        [SerializeField] private List<SpawnEnemyBatch> _enemies;
        [SerializeField] private Transform[] _spawnPositions;
        [SerializeField] private LayerCheck _enemyCheck;
        [SerializeField] private UnityEvent _finishEvent;

        private Coroutine _coroutine;
        private Random _random;
        private int _allCount;
        private bool finish;

        private void Start()
        {
            finish = false;
            _random =  new Random();
            foreach (var enemy in _enemies)
            {
                _allCount += enemy.AllCount;
            }
        }

        public void FixedUpdate()
        {
            if (_enemyCheck.IsTouchingLayer || _coroutine != null || finish) return;
            
            if (_allCount <= 0)
            {
                _finishEvent?.Invoke();
                finish = true;
            }
            else if (_coroutine == null)
            {
                _coroutine = StartCoroutine(SpawnEnemy());
            }
        }
        
        private IEnumerator SpawnEnemy()
        {
            foreach (var enemy in _enemies)
            {
                for (int i = 0; i < enemy.BatchCount; i++)
                {
                    var spawnPosition = _spawnPositions[_random.Next(0, _spawnPositions.Length)];
                    if (_allCount <= 0) break;
                    Instantiate(enemy.Prefab, spawnPosition.position, spawnPosition.rotation);
                    _allCount--;
                    yield return new WaitForSeconds(1);
                }
            }
            _coroutine = null;
        }
    }
    
    [Serializable]
    public struct SpawnEnemyBatch
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _allCount;
        [SerializeField] private int _batchCount;

        public GameObject Prefab => _prefab;

        public int AllCount => _allCount;

        public int BatchCount => _batchCount;
    }
}