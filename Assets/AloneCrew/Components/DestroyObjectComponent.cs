using UnityEngine;

namespace AloneCrew.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] GameObject _forDestroy;
        public void DoDestroy()
        {
            Destroy(_forDestroy);
        }
    }
}



