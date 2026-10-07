using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace AloneCrew.Effects.CameraRelated
{
    public class CameraShakeEffect : MonoBehaviour
    {
        [SerializeField] private float _animationTime = 0.3f;
        [SerializeField] private float _intensity = 3f;

        private CinemachineBasicMultiChannelPerlin _cameraShake;
        private Coroutine _shakeCoroutine;
        private void Awake()
        {
            var virtualCamera = GetComponent<CinemachineCamera>();
            _cameraShake = (CinemachineBasicMultiChannelPerlin) virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Noise);
        }

        public void Shake()
        {
            if (_shakeCoroutine != null)
            {
                ResetCamera();
                StopShakeCoroutine();
            }
            _shakeCoroutine = StartCoroutine(StartAnimation());
        }

        private IEnumerator StartAnimation()
        {
            _cameraShake.FrequencyGain = _intensity;
            yield return new WaitForSeconds(_animationTime);
            ResetCamera();
            StopShakeCoroutine();
        }
        
        private void StopShakeCoroutine()
        {
            StopCoroutine(_shakeCoroutine);
            _shakeCoroutine = null;
        }

        private void ResetCamera()
        {
            _cameraShake.FrequencyGain = 0;
        }
    }
}