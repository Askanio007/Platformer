using System.Collections;
using UnityEngine;

namespace AloneCrew
{
    public class MultiplyProjectileSpawner : AbstractProjectileSpawner
    {
        [SerializeField] private float _delayTimeBeforeAttack;

        public override IEnumerator SpawnProjectile()
        {
            yield return new WaitForSeconds(_delayTimeBeforeAttack);
            yield return base.SpawnProjectile();
        }

        protected override void Launch(Transform spawnPoint)
        {
            var pr = Instantiate(_projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            var direction = transform.localScale.x > 0 ? Vector3.left : Vector3.right;
            pr.transform.localScale = new Vector3(pr.transform.localScale.x * direction.x * (-1), pr.transform.localScale.y, pr.transform.localScale.z);
            Launch(pr, direction);
        }
    }
    
    
}