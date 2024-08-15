namespace Z3.CameraShake
{
    /// <summary>
    /// A container that contains data for a shake instance.
    /// Modifying this shake data will result in global changes.
    /// </summary>
    public class ShakeInstance
    {
        /// <summary>
        /// The original shake parameters.
        /// Note that modifying these parameters will overwrite the original values.
        /// </summary>
        public ShakeData ShakeParameters { get; }

        /// <summary>
        /// The current state of the shake.
        /// </summary>
        public ShakeState State { get; private set; }

        public bool IsFinished => State == ShakeState.Stopped;

        private float noiseTimer;
        private float fadeTransition;

        private float Duration => ShakeParameters.Duration;
        private float FadeInTime => ShakeParameters.FadeIn;
        private float FadeOutTime => ShakeParameters.FadeOut;

        public ShakeInstance(ShakeData parameters)
        {
            ShakeParameters = parameters;
            State = ShakeState.FadingIn;
        }

        /// <summary>
        /// Updates the shake timers and returns the resulting shake values.
        /// </summary>
        /// <param name="deltaTime">The delta time value to use. Typically this will be Time.deltaTime.</param>
        /// <returns>A ShakeResult containing the parameters of the current shake.</returns>
        public ShakeResult UpdateShake(float deltaTime)
        {
            // Update noise timer
            noiseTimer += deltaTime;

            // Update fade timer
            if (State == ShakeState.FadingIn)
            {
                fadeTransition += deltaTime / FadeInTime;
                if (fadeTransition >= 1f)
                {
                    fadeTransition = 1f;
                    State = ShakeState.Sustained;
                }
            }
            else if (State == ShakeState.Sustained)
            {
                if (noiseTimer >= FadeInTime + Duration)
                {
                    State = ShakeState.FadingOut;
                }
            }
            else if (State == ShakeState.FadingOut)
            {
                fadeTransition -= deltaTime / FadeOutTime; 
                if (fadeTransition <= 0f)
                {
                    fadeTransition = 0f;
                    State = ShakeState.Stopped;
                }
            }

            // Get shake values
            return new ShakeResult(ShakeParameters.NoiseSettings, fadeTransition);
        }
    }
}
