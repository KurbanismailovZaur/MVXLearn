using System.Collections;
using System.Collections.Generic;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace Azur.WindowsSystem.Switching
{
    /// <summary>
    /// Deactivates windows in parallel and then activates another window.
    /// </summary>
    [CreateAssetMenu(fileName = "ConcurrentlyWindowsSwitchingStrategy", menuName = "Configs/WindowsSystem/Switching/ConcurrentlyWindowsSwitchingStrategy")]
    public class ConcurrentlyWindowsSwitchingStrategy : WindowsSwitchingStrategy
    {
        public override IEnumerator SwitchEnumerator(List<Window> activatedWindows, Window deactivationWindow, 
            Window activationWindow, bool useUnscaledTime, int canvasSortOrderOffset, 
            WindowDeactivationAnimation windowDeactivationAnimation, WindowActivationAnimation windowActivationAnimation,
            MonoBehaviour coroutineOwner)
        {
            if (deactivationWindow != null)
            {
                var fromIndex = activatedWindows.IndexOf(deactivationWindow);
                var animations = new List<Coroutine>();
                
                for (var i = activatedWindows.Count - 1; i >= fromIndex; i--)
                {
                    var lastWindow = activatedWindows[i];
                    activatedWindows.RemoveAt(i);
                    var coroutine = lastWindow.Deactivate(useUnscaledTime, windowDeactivationAnimation, coroutineOwner);
                    animations.Add(coroutine);
                    
                    if (i - 1 >= 0)
                        activatedWindows[i - 1].GraphicRaycaster.enabled = true;
                }

                foreach (var animation in animations)
                    yield return animation;
            }
            
            if (activationWindow == null) 
                yield break;
            
            if (activatedWindows.Count > 0)
                activatedWindows[^1].GraphicRaycaster.enabled = false;
        
            activatedWindows.Add(activationWindow);
            activationWindow.Canvas.sortingOrder = canvasSortOrderOffset + activatedWindows.Count - 1;
            yield return activationWindow.Activate(useUnscaledTime, windowActivationAnimation, coroutineOwner);
        }
    }
}