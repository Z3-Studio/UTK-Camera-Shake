using Unity.Cinemachine;
using UnityEngine;

namespace Z3.CameraShake
{
    /// <summary>
    /// Apply shake in the associeted virtual camera
    /// </summary>
    public class ShakeListener : MonoBehaviour
    {
        [SerializeField] private CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;

        private void Reset()
        {
            if (!basicMultiChannelPerlin)
                TryGetComponent(out basicMultiChannelPerlin);
        }

        private void OnEnable()
        {
            basicMultiChannelPerlin.NoiseProfile = null;
            Shaker.OnUpdateShake += OnUpdateShake;
        }

        private void OnDisable()
        {
            Shaker.OnUpdateShake -= OnUpdateShake;
        }

        public void OnUpdateShake(ShakeResult result)
        {
            if (Shaker.ShakeActive)
            {
                basicMultiChannelPerlin.NoiseProfile = result;
                basicMultiChannelPerlin.AmplitudeGain = result.power;
                basicMultiChannelPerlin.FrequencyGain = result.power;
            }
            else
            {
                basicMultiChannelPerlin.NoiseProfile = null;
                basicMultiChannelPerlin.AmplitudeGain = 0f;
                basicMultiChannelPerlin.FrequencyGain = 0f;
            }
        }
    }
}