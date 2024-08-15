using Cinemachine;
using UnityEngine;
using Z3.UIBuilder.Core;

namespace Z3.CameraShake
{
    /// <summary>
    /// ScriptableObject to store ShakeParameters
    /// </summary>
    /// Note: CreateAssetMenu is created by Editor class
    public class ShakeData : ScriptableObject
    {
        [Title("Shake Parameters")]
        [SerializeField] private int priority;
        [Space]
        [Range(0, 10)]
        [SerializeField] private float fadeIn;
        [Min(0)]
        [SerializeField] private float duration = 1;
        [Range(0, 10)]
        [SerializeField] private float fadeOut;
        [Hide]
        [SerializeField] private NoiseSettings noiseSettings;

        public NoiseSettings NoiseSettings => noiseSettings;

        public const string NoiseSettingsField = nameof(noiseSettings);

        [ShowProperty]
        public float TotalDuration => fadeIn + duration + fadeOut;

        public int Priority => priority;
        public float FadeIn => fadeIn;
        public float FadeOut => fadeOut;
        public float Duration => duration;

        public void InvokeShaker() => Shaker.RequestShake(this);
    }
}

