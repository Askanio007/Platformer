using System;
using System.Collections;
using UnityEngine;

namespace AloneCrew
{
    public class CircularProjectileSpawner : MonoBehaviour
    {
        [SerializeField] protected CircularProjectileSetting[] _stages;
        private float _sectorAngle;
        public int Stage { get; set; }

        [ContextMenu("Launch")]
        public void LaunchProjectile()
        {
            StartCoroutine(SpawnProjectile());
        }

        public IEnumerator SpawnProjectile()
        {
            var setting = _stages[Stage];
            _sectorAngle = 2 * Mathf.PI / setting.Count;
            int counter = setting.Count;
            int angleModifier = 0;

            while (counter > 0)
            {
                for (int i = 0; i < setting.ItemPerBurst; i++)
                {
                    if (counter <= 0) break;
                    LaunchProjectile(angleModifier, setting);
                    counter--;
                    angleModifier++;
                }
                yield return new WaitForSeconds(setting.Delay);
            }
        }

        private void LaunchProjectile(int angleModifier, CircularProjectileSetting setting)
        {
            var angle = _sectorAngle * angleModifier;
            var direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            var pr = Instantiate(setting.ProjectilePrefab, setting.ProjectileSpawnPosition.position, Quaternion.identity);
            pr.GetComponent<DirectionalProjectile>().Launch(direction);
        }
    }

    [Serializable]
    public struct CircularProjectileSetting
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _projectileSpawnPosition;
        [SerializeField] private int _count;
        [SerializeField] private int _itemPerBurst;
        [SerializeField] private float _delay;

        public GameObject ProjectilePrefab => _projectilePrefab;

        public Transform ProjectileSpawnPosition => _projectileSpawnPosition;

        public int Count => _count;

        public int ItemPerBurst => _itemPerBurst;

        public float Delay => _delay;
    }
}