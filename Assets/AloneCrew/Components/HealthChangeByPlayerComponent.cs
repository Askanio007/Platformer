using AloneCrew.Components;
using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using UnityEngine;

namespace AloneCrew.Components
{
    public class HealthChangeByPlayerComponent : MonoBehaviour
    {
        private GameSession _session;
        protected void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
            if (_session == null)
            {
                Debug.LogError("No GameSession found");
            }
        }
        
        
        public void Modify(GameObject target)
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                var changeValue = _session != null ? (int) _session.GetRangeDamage() : 0; 
                healthComponent.ApplyDamage(changeValue);
            }
        
        }
    }
}
