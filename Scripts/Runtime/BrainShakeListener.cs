using UnityEngine;
using Unity.Cinemachine;
using Z3.UIBuilder.Core;
using static Unity.Cinemachine.ICinemachineCamera;

namespace Z3.CameraShake
{
    public class BrainShakeListener : MonoBehaviour
    {
        [Title("Shake Listener (Brain)")]
        [SerializeField] private CinemachineBrain brain;

        private CinemachineBasicMultiChannelPerlin currentPerlin;

        private ShakeResult lastResult = new();

        private void Reset()
        {
            if (!brain)
                TryGetComponent(out brain);
        }

        private void OnEnable()
        {
            CinemachineCore.CameraActivatedEvent.AddListener(OnCameraActivated);
            Shaker.OnUpdateShake += OnUpdateShake;

            UpdateCurrentPerlin(brain.ActiveVirtualCamera);
        }

        private void OnDisable()
        {
            Shaker.OnUpdateShake -= OnUpdateShake;
            CinemachineCore.CameraActivatedEvent.RemoveListener(OnCameraActivated);

            ClearPerlin();
        }

        private void OnUpdateShake(ShakeResult result)
        {
            lastResult = result;

            if (Shaker.ShakeActive)
            {
                if (!currentPerlin)
                    return;

                currentPerlin.NoiseProfile = result;
                currentPerlin.AmplitudeGain = result.power;
                currentPerlin.FrequencyGain = result.power;
            }
            else
            {
                ClearPerlin();
            }
        }

        private void OnCameraActivated(ActivationEventParams evt)
        {
            ClearPerlin(); // Remove evt.OutgoingCamera noise
            UpdateCurrentPerlin(evt.IncomingCamera);
            OnUpdateShake(lastResult);
        }

        private void UpdateCurrentPerlin(ICinemachineCamera camera)
        {
            CinemachineCamera vcam = camera as CinemachineCamera;
            if (vcam == null)
                return;            

            if (!vcam.gameObject.TryGetComponent(out CinemachineShakeListener _)) // Maybe cache it
            {
                currentPerlin = vcam.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
            }
            else
            {
                currentPerlin = null;
            }
        }

        private void ClearPerlin()
        {
            if (!currentPerlin)            
                return;

            currentPerlin.NoiseProfile = null;
            currentPerlin.AmplitudeGain = 0f;
            currentPerlin.FrequencyGain = 0f;
        }
    }
}
