using AloneCrew.Model;
using UnityEngine;

namespace AloneCrew
{
    public class DynamicCheckCircleOverlap : CheckCircleOverlap
    {
        private GameSession _session;

        protected void Start()
        {
            _session = FindFirstObjectByType<GameSession>();
            if (_session == null)
            {
                Debug.LogError("Not found GameSession for DynamicCheckCircleOverlap");
            }
        }
        protected override float GetRadius()
        {
            if (_session == null || !_session.PerksModel.IsAttackRangeSupported)
            {
                return base.GetRadius();
            }

            var extender = _session.PerksModel.GetUsedValue();
            return base.GetRadius() + (base.GetRadius() * extender);
        }
        
    }
}