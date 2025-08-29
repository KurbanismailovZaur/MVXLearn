using System.Collections;
using Azur.WindowsSystem;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace MVXLearn.UI.Animations
{
    [CreateAssetMenu(fileName = "FadingDeactivationWindowAnimation", menuName = "Configs/WindowsSystem/Animations/Fading/Deactivation")]
    public class FadingWindowDeactivationAnimation : WindowDeactivationAnimation
    {
        [SerializeField] private float _duration = 1f;
        
        public override IEnumerator AnimateEnumerator(Window window, bool useUnscaledTime)
        {
            var canvasGroup = (window as FadingWindow).CanvasGroup;

            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) / _duration;
                yield return null;
            }
            
            canvasGroup.alpha = 0f;
        }
    }
}