using Unity.Cinemachine;
using System.Linq;
using UnityEngine;
using static Unity.Cinemachine.NoiseSettings;

namespace Z3.CameraShake
{
    /// <summary>
    /// A data structure for holding shake parameters
    /// </summary>
    public class ShakeResult
    {
        /// <summary> Value between 0 ~ 1 </summary>
        public float power;
        public TransformNoiseParams[] positionNoise = new TransformNoiseParams[0];
        public TransformNoiseParams[] orientationNoise = new TransformNoiseParams[0];

        public ShakeResult() { }
        public ShakeResult(NoiseSettings noiseSettings, float power)
        {
            positionNoise = noiseSettings.PositionNoise;
            orientationNoise = noiseSettings.OrientationNoise;
            this.power = power;
        }

        public static ShakeResult operator +(ShakeResult a, ShakeResult b)
        {
            return new ShakeResult
            {
                positionNoise = a.positionNoise.Concat(b.positionNoise).ToArray(),
                orientationNoise = a.orientationNoise.Concat(b.orientationNoise).ToArray(),
                power = a.power > b.power ? a.power : b.power
            };
        }

        public static implicit operator NoiseSettings(ShakeResult shakeResult)
        {
            NoiseSettings noise = ScriptableObject.CreateInstance<NoiseSettings>();

            noise.PositionNoise = shakeResult.positionNoise;
            noise.OrientationNoise = shakeResult.orientationNoise;

            return noise;
        }
    }
}
