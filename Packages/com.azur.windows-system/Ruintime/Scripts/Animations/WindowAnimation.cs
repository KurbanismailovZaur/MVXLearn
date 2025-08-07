using System.Collections;
using UnityEngine;

namespace Azur.WindowsSystem.Animations
{
    /// <summary>
    /// Base class for any window animation.
    /// </summary>
    public abstract class WindowAnimation : ScriptableObject
    {
        /// <summary>
        /// This method will animate window when called.
        /// </summary>
        /// <param name="window">Window to be animated.</param>
        /// <param name="useUnscaledTime">Do we need use <c>Time.timeScale</c> independent animation?</param>
        public abstract IEnumerator AnimateEnumerator(Window window, bool useUnscaledTime);
    }
}