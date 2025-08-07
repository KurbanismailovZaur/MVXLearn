using System.Collections;
using System.Collections.Generic;
using Azur.WindowsSystem.Animations;
using UnityEngine;

namespace Azur.WindowsSystem.Switching
{
    /// <summary>
    /// Base class for any windows switching strategy. usually used in <c>WindowsManager</c>.
    /// </summary>
    public abstract class WindowsSwitchingStrategy : ScriptableObject
    {
        public abstract IEnumerator SwitchEnumerator(List<Window> activatedWindows, Window deactivationWindow, Window activationWindow, 
            bool useUnscaledTime, int canvasSortOrderOffset, WindowDeactivationAnimation windowDeactivationAnimation, 
            WindowActivationAnimation windowActivationAnimation, MonoBehaviour coroutineOwner);
    }
}