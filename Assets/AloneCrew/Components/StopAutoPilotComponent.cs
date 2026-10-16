using UnityEngine;

namespace AloneCrew.Components
{
    public class StopAutoPilotComponent : MonoBehaviour
    {

        public void Stop(GameObject gameObject)
        {
            var hero = gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                hero.Stop();
            }

        }
        
    }
}