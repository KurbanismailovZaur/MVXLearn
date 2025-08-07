using System.Collections;
using System.Collections.Generic;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace Azur.WindowsSystem.Switching
{
    /// <summary>
    /// Deactivates and activates windows in parallel.
    /// </summary>
    [CreateAssetMenu(fileName = "ImmediatelyWindowsSwitchingStrategy", menuName = "Configs/WindowsSystem/Switching/ImmediatelyWindowsSwitchingStrategy")]
    public class ImmediatelyWindowsSwitchingStrategy : WindowsSwitchingStrategy
    {
        public override IEnumerator SwitchEnumerator(List<Window> activatedWindows, Window deactivationWindow, 
            Window activationWindow, bool useUnscaledTime, int canvasSortOrderOffset, 
            WindowDeactivationAnimation windowDeactivationAnimation, WindowActivationAnimation windowActivationAnimation,
            MonoBehaviour coroutineOwner)
        {
            var animations = new List<Coroutine>();
            
            if (deactivationWindow != null)
            {
                var fromIndex = activatedWindows.IndexOf(deactivationWindow);
                
                for (var i = activatedWindows.Count - 1; i >= fromIndex; i--)
                {
                    var lastWindow = activatedWindows[i];
                    activatedWindows.RemoveAt(i);
                    animations.Add(lastWindow.Deactivate(useUnscaledTime, windowDeactivationAnimation, coroutineOwner));
                    
                    if (i - 1 >= 0)
                        activatedWindows[i - 1].GraphicRaycaster.enabled = true;
                }
            }

            if (activationWindow != null)
            {
                if (activatedWindows.Count > 0)
                    activatedWindows[^1].GraphicRaycaster.enabled = false;
        
                activatedWindows.Add(activationWindow);
                activationWindow.Canvas.sortingOrder = canvasSortOrderOffset + activatedWindows.Count - 1;
                var activationCoroutine = activationWindow.Activate(useUnscaledTime, windowActivationAnimation, coroutineOwner);
                
                animations.Add(activationCoroutine);
            }
            
            foreach (var animation in animations)
                yield return animation;
        }
    }
}