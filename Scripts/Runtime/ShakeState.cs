namespace Z3.CameraShake
{
    /// <summary>
    /// Represents the current state of a shake.
    /// </summary>
    public enum ShakeState
    {
        /// <summary>
        /// The shake is starting / fading in.
        /// </summary>
        FadingIn,

        /// <summary>
        /// The shake has reached its full strength and is now constant.
        /// </summary>
        Sustained,

        /// <summary>
        /// The shake is stopping / fading out.
        /// </summary>
        FadingOut,

        /// <summary>
        /// The shake has fully stopped.
        /// </summary>
        Stopped
    }
}
