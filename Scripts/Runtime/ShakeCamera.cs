using UnityEngine;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace Z3.CameraShake
{
    [NodeCategory(Categories.NodeGraph + "/Camera Shake")]
    [NodeDescription("Shakes camera with ShakeData")]
    public class ShakeCamera : ActionTask
    {
        [SerializeField] private Parameter<ShakeData> shakeData;
        [SerializeField] private Parameter<bool> waitUntilFinish;

        public override string Info => $"Shake {shakeData}";

        private float shakeDuration;

        protected override void StartAction()
        {
            Shaker.RequestShake(shakeData.Value);

            if (waitUntilFinish.Value)
            {
                shakeDuration = shakeData.Value.Duration;
            }
            else
            {
                EndAction();
            }
        }

        protected override void UpdateAction()
        {
            if (NodeRunningTime >= shakeDuration)
            {
                EndAction();
            }
        }
    }
}