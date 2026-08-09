using System;
using UnityEngine;

namespace AloneCrew.Components
{
    public class PlaySoundComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioData[] _sounds;

        public void Play(string id)
        {
            foreach (AudioData data in _sounds)
            {
                if (data.Id == id)
                {
                    _source.PlayOneShot(data.Clip);
                    break;
                }
            }
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