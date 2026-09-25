using AloneCrew.Components.Audio;
using UnityEngine;
using UnityEngine.Audio;

namespace AloneCrew.Components
{
    
    public class AudioMixerZoneComponent : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private AudioMixerGroup _audioMixerOnLeave;
        [SerializeField] private string _tag;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;
            UpdateAudioMixer(_audioMixer);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!string.IsNullOrEmpty(_tag) && !collision.gameObject.CompareTag(_tag)) return;
            UpdateAudioMixer(_audioMixerOnLeave);
        }

        private void UpdateAudioMixer(AudioMixerGroup audioMixerGroup)
        {
            var audioSource = FindObjectsByType<AudioSettingComponent>(FindObjectsSortMode.None);
            foreach (var s in audioSource)
            {
                var source = s.GetComponent<AudioSource>();
                if (source == null) continue;
                source.outputAudioMixerGroup = audioMixerGroup;
            }
        }
    }
}

