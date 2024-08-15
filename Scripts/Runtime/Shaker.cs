using System;
using UnityEngine;
using Z3.Utils;

namespace Z3.CameraShake
{
    /// <summary>
    /// Receive the shake parameters and send them to the listeners
    /// </summary>
    public class Shaker : Monostate<Shaker>
    {
        public static event Action<ShakeResult> OnUpdateShake;
        public static bool ShakeActive { get; private set; } = true;

        private ShakeInstance currentShake;

        public static void SetActiveShake(bool enable)
        {
            ShakeActive = enable;

            if (!enable)
                UpdateShake(new ShakeResult());
        }
            
        public static void RequestShake(ShakeData shake) => Instance.OnShake(shake);

        private void OnShake(ShakeData shake)
        {
            if (currentShake != null && currentShake.ShakeParameters.Priority > shake.Priority)
                return;

            currentShake = new ShakeInstance(shake);
        }

        private void Update()
        {
            if (currentShake == null)
                return;

            ShakeResult shake = currentShake.UpdateShake(Time.deltaTime);

            if (currentShake.IsFinished)
            {
                currentShake = null;
            }

            UpdateShake(shake);
        }

        public static void UpdateShake(ShakeResult shake) => OnUpdateShake?.Invoke(shake); // IMPORTANT: Used in gamepad rumble 
    }
}