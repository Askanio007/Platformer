using System;
using System.Linq;
using UnityEditor.AdaptivePerformance.Editor;
using UnityEngine;

namespace AloneCrew.Components
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[]  _spawns;

        public void Spawn(string id)
        {
            var spawner = _spawns.FirstOrDefault(x => x.Id == id);
            spawner?.Component.Spawn();
        }
        
        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent Component;

        }
    }
}