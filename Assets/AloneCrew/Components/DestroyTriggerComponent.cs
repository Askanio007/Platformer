using AloneCrew.Utils.ObjectPool;
using UnityEngine;

namespace AloneCrew.Components
{
    
    public class DestroyTriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.activeInHierarchy) return;
            if ((_layer.value & (1 << collision.gameObject.layer)) == 0) return;
            var poolItem = collision.gameObject.GetComponent<PoolItem>();
            if (poolItem != null)
            {
                poolItem.Release();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }

    }
}

