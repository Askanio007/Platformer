using System;
using UnityEngine;

namespace AloneCrew.Components
{
    public class PlaySoundComponent : MonoBehaviour
    {
        private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;

        public void Start()
        {
            InitSource();
        }

        public void Play(string id)
        {
            foreach (AudioData data in _sounds)
            {
                if (data.Id == id)
                {
                    if (_source == null)
                    {
                        InitSource();
                    }
                    _source.PlayOneShot(data.Clip);
                    break;
                }
            }
        }

        private void InitSource()
        {
            _source = GameObject.FindGameObjectWithTag("SfxAudioSource").GetComponent<AudioSource>();
        }
        
        
        [Serializable]
        public class AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;
            
            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }
}