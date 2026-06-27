using AloneCrew.Components;
using UnityEngine;

namespace AloneCrew.Components
{
    public class HealthChangeComponent : MonoBehaviour
    {
        [SerializeField] private int _changeValue;

        public void Modify(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(_changeValue);
            }
        
        }
    }
}
