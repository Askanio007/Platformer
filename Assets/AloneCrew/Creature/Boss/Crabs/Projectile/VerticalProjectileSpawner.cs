using System;
using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace AloneCrew
{
    public class VerticalProjectileSpawner : AbstractProjectileSpawner
    {

        [ContextMenu("Launch")]
        public void LaunchProjectile()
        {
            base.LaunchProjectile(() => {});
        }
        
        protected override void Launch(Transform spawnPoint)
        {
            var pr = Instantiate(_projectilePrefab, spawnPoint.position, _projectilePrefab.transform.rotation);
            Launch(pr, Vector2.down);
        }
    }
    
    
}