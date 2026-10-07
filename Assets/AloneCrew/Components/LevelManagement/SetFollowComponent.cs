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
            var hero = FindFirstObjectByType<Hero>();
            if (hero != null)
            {
                vCamera.Follow = hero.transform;
            }
            else
            {
                Debug.LogWarning("Not found Hero for camera");
            }
        }
        
    }
}