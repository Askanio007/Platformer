using AloneCrew.Model.Data;
using UnityEngine;

namespace AloneCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _initData;

        private void Awake()
        {
            var existSession = GetSessionExist();
            if (existSession != null)
            {
                existSession._initData = existSession.Data.Clone();
                DestroyImmediate(gameObject);
            }
            else
            {
                _initData = Data.Clone();
                DontDestroyOnLoad(gameObject);
            }
        }

        public void Reset()
        {
            Data.Hp = _initData.Hp;
            Data.IsArmed =  _initData.IsArmed;
        }

        private GameSession GetSessionExist()
        {
            var sessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None);
            foreach (var s in sessions)
            {
                if (s != this)
                {
                    return s;
                }
            }
            return null;
        }
        
    }
}