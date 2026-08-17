using AloneCrew.Model.Data.Properties;
using Unity.VisualScripting;
using UnityEngine;

namespace AloneCrew.Model.Data
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Data/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;
        private static GameSettings _instance;
        public static GameSettings I => _instance == null ? LoadSettings() : _instance;

        private static GameSettings LoadSettings()
        {
            return Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1f, SoundSetting.Music.ToString());
            _sfx = new FloatPersistentProperty(1f, SoundSetting.Sfx.ToString());
        }
        
        
    }
}