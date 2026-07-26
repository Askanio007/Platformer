using UnityEngine;

namespace AloneCrew
{
    public class SwordGrabber : MonoBehaviour
    {
        [SerializeField] private int _sword;

        public void GrabSword(GameObject gameObject)
        {
            var hero = gameObject.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddSword(_sword);
            }
        }
        
    }
}