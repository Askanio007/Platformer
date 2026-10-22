using AloneCrew.Utils;
using AloneCrew.Utils.ObjectPool;
using UnityEngine;

namespace AloneCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {

        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _direction;
        [SerializeField] private bool _usePool;
        [SerializeField] private bool _usePrefabScale;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = _usePool ? 
                Pool.Instance.Get(_prefab, _target.position) : 
                SpawnUtils.Spawn(_prefab, _target.position);
            if (!_usePrefabScale)
            {
                instantiate.transform.localScale = _target.lossyScale;
            }
            if (_direction != null)
            {
                instantiate.GetComponent<Creature>()?.SetDirectionAuto(Vector2.right);
            }
        }

        public void SetPrefabAndSpawn(GameObject prefab)
        {
            _prefab = prefab;
            Spawn();
        }

    }
}