using AloneCrew.Components;
using UnityEngine;

namespace AloneCrew.Components
{
    public class JumpSpeedChangeComponent : MonoBehaviour
    {
        [SerializeField] private float _changeValue;

        public void Modify(GameObject target)
        {
            var hero = target.GetComponent<Hero>();
            if (hero != null)
            {
                hero.SetJumpSpeed(_changeValue);
            }
        
        }
    }
}
