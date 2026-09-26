using System;
using AloneCrew.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components.LevelManagement
{
    [RequireComponent(typeof(SpawnComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private UnityEvent _setChecked;
        [SerializeField] private UnityEvent _setUnchecked;
        [SerializeField] private SpawnComponent _spawnComponent;
        private GameSession _gameSession;
        
        public string Id => _id;

        public void Start()
        {
            _gameSession = FindFirstObjectByType<GameSession>();
            if (_gameSession.IsChecked(_id))
            {
                _setChecked?.Invoke();
            }
            else
            {
                _setUnchecked?.Invoke();
            }
        }

        public void Check()
        {
            _gameSession.AddCheckpoint(_id);
            _setChecked?.Invoke();
        }

        public void SpawnHero()
        {
            _spawnComponent.Spawn();
        }
    }
}