using System.Collections;
using Azur.WindowsSystem;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace MVXLearn.UI.Animations
{
    [CreateAssetMenu(fileName = "FadingActivationWindowAnimation", menuName = "Configs/WindowsSystem/Animations/Fading/Activation")]
    public class FadingWindowActivationAnimation : WindowActivationAnimation
    {
        [SerializeField] private float _duration = 1f;
        
        public override IEnumerator AnimateEnumerator(Window window, bool useUnscaledTime)
        {
            var canvasGroup = (window as FadingWindow).CanvasGroup;

            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += (useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime) / _duration;
                yield return null;
            }
            
            canvasGroup.alpha = 1f;
        }
    }
}