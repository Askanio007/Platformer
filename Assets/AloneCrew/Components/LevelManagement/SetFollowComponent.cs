using Unity.Cinemachine;
using UnityEngine;

namespace AloneCrew.Components.LevelManagement
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class SetFollowComponent : MonoBehaviour
    {

        public void Start()
        {
            var vCamera = GetComponent<CinemachineCamera>();
            vCamera.Follow = FindFirstObjectByType<Hero>().transform;
        }
        
    }
}