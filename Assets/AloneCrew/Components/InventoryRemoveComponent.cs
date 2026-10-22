using AloneCrew.Model;
using AloneCrew.Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace AloneCrew.Components
{
    public class InventoryRemoveComponent : MonoBehaviour
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private int _count;
        
        [SerializeField] private UnityEvent _onComplete;

        public void Remove()
        {
            var session = FindFirstObjectByType<GameSession>();
            if (session == null)
            {
                Debug.LogWarning("No GameSession found");
                return;
            }
            session.RemoveFromInventory(_id, _count);
            _onComplete?.Invoke();
        }
    }
}