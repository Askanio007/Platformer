using System;
using UnityEngine;

namespace AloneCrew.Model
{
    [Serializable]
    public class CoinSpawnModel
    {
        public int Count;
        public int Possibility;
        public GameObject Prefab;
    }
}