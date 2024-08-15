using UnityEngine;

namespace Z3.CameraShake
{
    /// <summary>
    /// Shake trigger for animation clips.
    /// </summary>
    public class ShakeTrigger : MonoBehaviour 
    {
        public void Shake(ShakeData shakeData) => Shaker.RequestShake(shakeData); 
    }
}