using UnityEngine;

namespace AloneCrew
{
    public class CoinGrabber : MonoBehaviour
    {
        [SerializeField] private int _coin;

        public void GrabCoin(Collider2D collider2D)
        {
            var hero = collider2D.GetComponent<Hero>();
            if (hero != null)
            {
                hero.AddCoin(_coin);
            }
        }
        
    }
}