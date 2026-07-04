using System.Collections.Generic;
using AloneCrew.Model;
using UnityEngine;

namespace AloneCrew.Components
{
    public class CoinSpawnComponent : MonoBehaviour
    {

        [SerializeField] private Transform _target;
        [SerializeField] private List<CoinSpawnModel> _coins;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            foreach (var coin in _coins)
            {
                for (int i = 0; i < coin.Count; i++)
                {
                    if (coin.Possibility >= Random.Range(0, 100))
                    {
                        var instantiate = Instantiate(coin.Prefab, _target.position, Quaternion.identity);
                        instantiate.transform.localScale = _target.lossyScale;
                    }
                }
            }
            
        }

    }
}