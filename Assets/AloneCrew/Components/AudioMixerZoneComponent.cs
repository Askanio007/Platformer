using UnityEngine;
using UnityEngine.Audio;

namespace AloneCrew.Components
{
    
    public class AudioMixerZoneComponent : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private AudioMixerGroup _audioMixerOnLeave;
        [SerializeField] private string _tag;
        [SerializeField] private AudioSource[] _sources;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;
            foreach (var s in _sources)
            {
                s.outputAudioMixerGroup = _audioMixer;
            }
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;
            foreach (var s in _sources)
            {
                s.outputAudioMixerGroup = _audioMixerOnLeave;
            }
        }

    }
}

