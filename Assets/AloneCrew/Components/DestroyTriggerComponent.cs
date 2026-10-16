using UnityEngine;

namespace AloneCrew.Components
{
    
    public class DestroyTriggerComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer = ~0;
        private void OnTriggerExit2D(Collider2D collision)
        {
            if ((_layer.value & (1 << collision.gameObject.layer)) == 0) return;
            Destroy(collision.gameObject);
        }

    }
}

