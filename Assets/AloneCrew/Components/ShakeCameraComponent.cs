using System;
using AloneCrew.Effects.CameraRelated;
using UnityEngine;

namespace AloneCrew.Components
{
    public class ShakeCameraComponent : MonoBehaviour
    {
        private CameraShakeEffect _cameraShakeEffect;
        private void Start()
        {
            _cameraShakeEffect = FindFirstObjectByType<CameraShakeEffect>();
        }

        public void ShakeCamera()
        {
            if (_cameraShakeEffect != null)
            {
                _cameraShakeEffect.Shake();
            }
            else
            {
                Debug.Log("No CameraShakeEffect found");
            }
        }
    }
}