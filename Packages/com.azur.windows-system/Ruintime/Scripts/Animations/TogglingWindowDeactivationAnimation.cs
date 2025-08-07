using System.Collections;
using UnityEngine;

namespace Azur.WindowsSystem.Animations
{
    /// <summary>
    /// Represent immediately toggling deactivation animation.
    /// </summary>
    [CreateAssetMenu(fileName = "TogglingDeactivationWindowAnimation", menuName = "Configs/WindowsSystem/Animations/Toggling/Deactivation")]
    public class TogglingWindowDeactivationAnimation : WindowDeactivationAnimation
    {
        public override IEnumerator AnimateEnumerator(Window window, bool useUnscaledTime)
        {
            yield break;
        }
    }
}