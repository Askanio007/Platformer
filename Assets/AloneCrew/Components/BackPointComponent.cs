using UnityEngine;

namespace AloneCrew.Components
{
    public class BackPointComponent : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        public Transform Transform => _transform;
    }
}