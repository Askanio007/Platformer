using AloneCrew.Components;
using UnityEngine;

namespace AloneCrew
{
    public class CrabsController : MonoBehaviour
    {
        [SerializeField] private SpawnComponent _fallParticleSpawner;
        [SerializeField] private PlaySoundComponent _playSound;

        public void SpawnFallParticles()
        {
            _fallParticleSpawner.Spawn();
            _playSound.Play("fall");
        }
    }
}