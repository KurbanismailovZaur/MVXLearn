using System.Collections;
using UnityEngine;

namespace Azur.WindowsSystem.Animations
{
    /// <summary>
    /// Represent immediately toggling activation animation.
    /// </summary>
    [CreateAssetMenu(fileName = "TogglingActivationWindowAnimation", menuName = "Configs/WindowsSystem/Animations/Toggling/Activation")]
    public class TogglingWindowActivationAnimation : WindowActivationAnimation
    {
        public override IEnumerator AnimateEnumerator(Window window, bool useUnscaledTime)
        {
            yield break;
        }
    }
}