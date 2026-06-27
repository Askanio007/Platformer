using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] GameObject _forDestroy;
    public void DoDestroy()
    {
        Destroy(_forDestroy);
    }
}
