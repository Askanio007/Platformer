using AloneCrew.Utils;
using UnityEngine;

namespace AloneCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {

        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _direction;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefab, _target.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;
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