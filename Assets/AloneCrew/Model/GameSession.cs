using AloneCrew.Model.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        private PlayerData _initData;

        private void Awake()
        {
            LoadHud();
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

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
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