using UnityEngine;

public class CoinGrabber : MonoBehaviour
{
    [SerializeField] private int _coinValue;

    public void DoGrab()
    {
        Debug.Log($"Get {_coinValue}");
        
    }
}
