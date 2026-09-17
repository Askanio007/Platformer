using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Interaction
{
    public class RequireItemComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;
        
        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;

        public void Check()
        {
            var session = FindFirstObjectByType<GameSession>();
            if (session == null)
            {
                _onFail?.Invoke();
                Debug.LogWarning("No GameSession found");
                return;
            }
            
            var numItems = session.Data.Inventory.Count(_id);
            if (numItems >= _count)
            {
                _onSuccess?.Invoke();
            }
            else
            {
                _onFail?.Invoke();
            }

        }
        
    }
}