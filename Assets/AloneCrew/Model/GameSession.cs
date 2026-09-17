using System;
using AloneCrew.Model.Data;
using AloneCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneCrew.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;
        public int Hp
        {
            get { return Data.Hp.Value; }
            set { Data.Hp.Value = value; }
        }
        
        public bool IsArmed
        {
            get { return Data.IsArmed; }
            set { Data.IsArmed = value; }
        }
        
        private PlayerData _initData;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public QuickInventoryModel QuickInventoryModel { get;  private set; }
        public string QuickInvSelectedItemId => QuickInventoryModel.SelectedItem.Id;

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
                InitModels();
                DontDestroyOnLoad(gameObject);
            }
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        private void InitModels()
        {
            QuickInventoryModel = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventoryModel);
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

        public bool AddToInventory(string id, int value)
        {
            return Data.Inventory.Add(id, value);
        }
        
        public void RemoveFromInventory(string id, int count)
        {
            Data.Inventory.Remove(id, count);
        }
        
        public long CountInInventory(string id)
        {
            return Data.Inventory.Count(id);
        }
        
        public void SubscribeOnInventoryChanged(InventoryData.OnInventoryChanged method)
        {
            Data.Inventory.onInventoryChanged += method;
        }
        
        public void UnsubscribeOnInventoryChanged(InventoryData.OnInventoryChanged method)
        {
            Data.Inventory.onInventoryChanged -= method;
        }
        
        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}