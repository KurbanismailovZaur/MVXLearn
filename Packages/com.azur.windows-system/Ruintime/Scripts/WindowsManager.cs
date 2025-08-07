using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Azur.WindowsSystem;
using Azur.WindowsSystem.Animations;
using Azur.WindowsSystem.Switching;
using UnityEngine;
using UnityEngine.Serialization;

namespace Azur.WindowsSystem
{
    /// <summary>
    /// Manages the creation, activation, deactivation, and caching of windows.
    /// </summary>
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField] private Transform _defaultWindowsParent;
        [SerializeField] private WindowActivationAnimation _defaultWindowActivationAnimation;
        [SerializeField] private WindowDeactivationAnimation _defaultWindowDeactivationAnimation;
        [SerializeField] private WindowsSwitchingStrategy _defaultWindowsSwitchingStrategy;
        [SerializeField] private int _canvasSortOrderOffset;
        [SerializeField] private List<Window> _activatedWindows = new();
        [SerializeField] private List<Window> _cachedWindows = new();
        [SerializeField] private List<Window> _prefabWindows = new();

        private bool _isInTransition;
        
        /// <summary>
        /// It will return <c>true</c> if the window manager is currently activating, deactivating, or switching any windows.
        /// </summary>
        public bool IsInTransition => _isInTransition;

        /// <summary>
        /// The offset for the canvas sort order of windows. Whenever the window manager activates a window,
        /// it assigns its <c>canvas.sortingOrder</c> a value equal to <c>CanvasSortOrderOffset + ActivatedWindows.Count - 1</c>.
        /// </summary>
        public int CanvasSortOrderOffset => _canvasSortOrderOffset;

        /// <summary>
        /// Returns a read-only list of active windows. The list items are sorted according to the order in which
        /// the windows were activated. The last item is the window currently visible to the user.
        /// </summary>
        public ReadOnlyCollection<Window> ActivatedWindows => _activatedWindows.AsReadOnly();
        
        /// <summary>
        /// Returns last window from <c>ActivatedWindows</c>.
        /// </summary>
        public Window CurrentWindow => _activatedWindows.LastOrDefault();

        /// <summary>
        /// Allows you to create a window manager at runtime.
        /// </summary>
        /// <param name="defaultWindowsParent">Default parent for new instantiated windows. If <c>null</c>, then the windows will be created directly in the scene.</param>
        /// <param name="defaultWindowActivationAnimation">Default activation animation. If <c>null</c> then the window manager will use the animation specified for a specific window.</param>
        /// <param name="defaultWindowDeactivationAnimation">Default deactivation animation. If <c>null</c> then the window manager will use the animation specified for a specific window.</param>
        /// <param name="defaultWindowsSwitchingStrategy">Default windows switching strategy. Can't be <c>null</c>.</param>
        /// <param name="canvasSortOrderOffset">Canvas sorting order offset for managed windows.</param>
        /// <param name="activatedWindows">List of currently activated windows.</param>
        /// <param name="cachedWindows">List of currently cached windows (activated or not).</param>
        /// <param name="prefabWindows">List of windows prefabs, which will be used when windows manager need to instantiate new one.</param>
        /// <returns></returns>
        public static WindowsManager Instantiate(Transform defaultWindowsParent, 
            WindowActivationAnimation defaultWindowActivationAnimation,
            WindowDeactivationAnimation defaultWindowDeactivationAnimation, WindowsSwitchingStrategy defaultWindowsSwitchingStrategy,
            int canvasSortOrderOffset, List<Window> activatedWindows, List<Window> cachedWindows, 
            List<Window> prefabWindows)
        {
            var windowsManager = new GameObject("WindowsManager").AddComponent<WindowsManager>();
            windowsManager._defaultWindowsParent = defaultWindowsParent;
            windowsManager._defaultWindowActivationAnimation = defaultWindowActivationAnimation;
            windowsManager._defaultWindowDeactivationAnimation = defaultWindowDeactivationAnimation;
            windowsManager._defaultWindowsSwitchingStrategy = defaultWindowsSwitchingStrategy;
            windowsManager._canvasSortOrderOffset = canvasSortOrderOffset;
            windowsManager._activatedWindows = activatedWindows;
            windowsManager._cachedWindows = cachedWindows;
            windowsManager._prefabWindows = prefabWindows;

            return windowsManager;
        }
        
        /// <summary>
        /// Gets activated window by type.
        /// </summary>
        public TWindow GetActivatedWindow<TWindow>() where TWindow : Window
        {
            return (TWindow)_activatedWindows.LastOrDefault(w => w is TWindow);
        }

        /// <summary>
        /// Gets activated window by type and predicate.
        /// </summary>
        public TWindow GetActivatedWindow<TWindow>(Func<TWindow, bool> predicate) where TWindow : Window
        {
            return (TWindow)_activatedWindows.LastOrDefault(w => w is TWindow window && predicate(window));
        }

        /// <summary>
        /// Gets activated windows by type.
        /// </summary>
        public List<TWindow> GetActivatedWindows<TWindow>() where TWindow : Window
        {
            return _activatedWindows.Where(w => w is TWindow).Cast<TWindow>().ToList();
        }
        
        /// <summary>
        /// Gets activated windows by type and predicate.
        /// </summary>
        public List<TWindow> GetActivatedWindows<TWindow>(Func<TWindow, bool> predicate) where TWindow : Window
        {
            return  GetActivatedWindows<TWindow>().Where(predicate).ToList();
        }
        
        /// <summary>
        /// Cache new window by type. Window will be found in <c>PrefabWindows</c> list.
        /// <param name="parent">Parent for instantiated window.</param>
        /// </summary>
        public TWindow CacheNewWindow<TWindow>(RectTransform parent = null) where TWindow : Window
        {
            var windowPrefab = (TWindow)_prefabWindows.First(w => w is TWindow);
            var window = Instantiate(windowPrefab, parent ?? _defaultWindowsParent);
            _cachedWindows.Add(window);
            Window.ProtectedMembersAccessor.CallOnInstantiated(window);

            return window;
        }
        
        /// <summary>
        /// Gets cached window by type (activated or not).
        /// </summary>
        public TWindow GetCachedWindow<TWindow>() where TWindow : Window
        {
            return (TWindow)_cachedWindows.FirstOrDefault(w => w is TWindow);
        }

        /// <summary>
        /// Gets cached window by type (activated or not) and predicate.
        /// </summary>
        public TWindow GetCachedWindow<TWindow>(Func<TWindow, bool> predicate) where TWindow : Window
        {
            return (TWindow)_cachedWindows.FirstOrDefault(w => w is TWindow window && predicate(window));
        }

        /// <summary>
        /// Gets cached window by type (activated or not). If window is not found, then try instatiate new one via <c>CacheNewWindow</c> method.
        /// </summary>
        public TWindow GetOrCreateCachedWindow<TWindow>() where TWindow : Window
        {
            return GetCachedWindow<TWindow>() ?? CacheNewWindow<TWindow>();
        }

        /// <summary>
        /// Gets cached window by type (activated or not) and predicate. If window is not found, then try instatiate new one via <c>CacheNewWindow</c> method.
        /// </summary>
        public TWindow GetOrCreateCachedWindow<TWindow>(Func<TWindow, bool> predicate) where TWindow : Window
        {
            return GetCachedWindow(predicate) ?? CacheNewWindow<TWindow>();
        }

        private void CheckAndSetTransitionState()
        {
            if (_isInTransition)
                throw new InvalidOperationException("Window manager is already in transition state.");
            
            _isInTransition = true;
        }

        /// <summary>
        /// Activate window by type on top of the other windows.
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowActivationAnimation">Window activation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// </summary>
        public Coroutine ActivateWindow<TWindow>(bool useUnscaledTime = true, 
            WindowActivationAnimation windowActivationAnimation = null) where TWindow : Window
        {
            CheckAndSetTransitionState();
            
            var window = GetOrCreateCachedWindow<TWindow>(w => !w.gameObject.activeSelf);
            windowActivationAnimation ??= _defaultWindowActivationAnimation;
            
            return StartCoroutine(SwitchWindowsEnumerator(null, window, useUnscaledTime, _defaultWindowsSwitchingStrategy, 
                null, windowActivationAnimation));
        }
        
        /// <summary>
        /// Deactivates (from the end) all windows in the list of activated ones up to the window with the specified <c>TWindow</c> type.
        /// <param name="deactivateTargetWindow">Do we need to deactivate the target window (the one that matches the <c>TWindow</c> type)?</param>
        /// <param name="windowIndexFromEnd">Up to what number of <c>TWindow</c> type windows should deactivate, if there are several such windows? By default, it is 0, which means the first from the end.</param>
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowsSwitchingStrategy">window switching strategy. This method uses it as a window deactivation strategy. If not specified, the one set in the window manager is used.</param>
        /// <param name="windowDeactivationAnimation">Window deactivation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// </summary>
        public Coroutine DeactivateWindowsUpTo<TWindow>(bool deactivateTargetWindow = false, int windowIndexFromEnd = 0, 
            bool useUnscaledTime = true, WindowsSwitchingStrategy windowsSwitchingStrategy = null, 
            WindowDeactivationAnimation windowDeactivationAnimation = null) 
            where TWindow : Window
        {
            CheckAndSetTransitionState();
            
            var windows = _activatedWindows.Where(w => w is TWindow).Cast<TWindow>().ToList();
            Window window = windows[^(windowIndexFromEnd + 1)];

            if (!deactivateTargetWindow)
            {
                var index = _activatedWindows.IndexOf(window);
                index += 1;
                
                window = index > _activatedWindows.Count - 1 ? null : _activatedWindows[index];
            }

            windowsSwitchingStrategy ??= _defaultWindowsSwitchingStrategy;
            windowDeactivationAnimation ??= _defaultWindowDeactivationAnimation;
            
            return StartCoroutine(SwitchWindowsEnumerator(window, null, useUnscaledTime, windowsSwitchingStrategy, 
                windowDeactivationAnimation, null));
        }

        /// <summary>
        /// Deactivate current window.
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowDeactivationAnimation">Window deactivation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// </summary>
        public Coroutine DeactivateCurrentWindow(bool useUnscaledTime = true, 
            WindowDeactivationAnimation windowDeactivationAnimation = null)
        {
            CheckAndSetTransitionState();
            
            var window = CurrentWindow;

            if (window == null)
                return null;
            
            windowDeactivationAnimation ??= _defaultWindowDeactivationAnimation;
            
            return StartCoroutine(SwitchWindowsEnumerator(window, null, useUnscaledTime, _defaultWindowsSwitchingStrategy, 
                windowDeactivationAnimation, null));
        }

        /// <summary>
        /// Deactivate all windows.
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowsSwitchingStrategy">window switching strategy. This method uses it as a window deactivation strategy. If not specified, the one set in the window manager is used.</param>
        /// <param name="windowDeactivationAnimation">Window deactivation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// </summary>
        public Coroutine DeactivateAllWindows(bool useUnscaledTime = true, WindowsSwitchingStrategy windowsSwitchingStrategy = null, 
            WindowDeactivationAnimation windowDeactivationAnimation = null)
        {
            CheckAndSetTransitionState();
            
            if (_activatedWindows.Count == 0)
                return null;
            
            var window = _activatedWindows[0];
            windowsSwitchingStrategy ??= _defaultWindowsSwitchingStrategy;
            windowDeactivationAnimation ??= _defaultWindowDeactivationAnimation;
            
            return StartCoroutine(SwitchWindowsEnumerator(window, null, useUnscaledTime, windowsSwitchingStrategy, 
                windowDeactivationAnimation, null));
        }
        
        /// <summary>
        /// Deactivates (from the end) all windows in the list of activated ones up to the window with the specified <c>TDeactivationWindow</c> type and activate window with specified <c>TActivationWindow</c> type.
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowIndexFromEnd">Up to what number of <c>TWindow</c> type windows should deactivate, if there are several such windows? By default, it is 0, which means the first from the end.</param>
        /// <param name="windowsSwitchingStrategy">window switching strategy. This method uses it as a window deactivation strategy. If not specified, the one set in the window manager is used.</param>
        /// <param name="windowDeactivationAnimation">Window deactivation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// <param name="windowActivationAnimation">Window activation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// </summary>
        public Coroutine SwitchWindows<TDeactivationWindow, TActivationWindow>(bool useUnscaledTime = true, int windowIndexFromEnd = 0,
            WindowsSwitchingStrategy windowsSwitchingStrategy = null, WindowDeactivationAnimation windowDeactivationAnimation = null, 
            WindowActivationAnimation windowActivationAnimation = null) 
            where TDeactivationWindow : Window 
            where TActivationWindow : Window
        {
            CheckAndSetTransitionState();
            
            var windows = _activatedWindows.Where(w => w is TDeactivationWindow).Cast<TDeactivationWindow>().ToList();
            Window deactivationWindow = windows[^(windowIndexFromEnd + 1)];
            var activationWindow = GetOrCreateCachedWindow<TActivationWindow>(w => !w.gameObject.activeSelf);

            windowsSwitchingStrategy ??= _defaultWindowsSwitchingStrategy;
            windowActivationAnimation ??= _defaultWindowActivationAnimation;
            windowDeactivationAnimation ??= _defaultWindowDeactivationAnimation;
            
            return StartCoroutine(SwitchWindowsEnumerator(deactivationWindow, activationWindow, useUnscaledTime, windowsSwitchingStrategy, 
                windowDeactivationAnimation, windowActivationAnimation));
        }
        
        
        private IEnumerator SwitchWindowsEnumerator(Window deactivationWindow, Window activationWindow, 
            bool useUnscaledTime, WindowsSwitchingStrategy windowsSwitchingStrategy, 
            WindowDeactivationAnimation windowDeactivationAnimation, WindowActivationAnimation windowActivationAnimation)
        {
            var enumerator = windowsSwitchingStrategy.SwitchEnumerator(_activatedWindows, deactivationWindow, 
                activationWindow, useUnscaledTime, _canvasSortOrderOffset, windowDeactivationAnimation, 
                windowActivationAnimation, this);

            while (enumerator.MoveNext())
                yield return enumerator.Current;
            
            _isInTransition = false;
        }

        /// <summary>
        /// Sets canvas sort order for all cached windows according to their hierarchy.
        /// </summary>
        [ContextMenu("Sort Windows By Hierarchy")]
        public void SetWindowsSortOrderByHierarchy()
        {
            var windows = _cachedWindows.OrderBy(w => GetGlobalSiblingIndex(w.transform)).ToList();
            
            for (var i = 0; i < windows.Count; i++)
                windows[i].Canvas.sortingOrder = _canvasSortOrderOffset + i;
            
            return;

            static int GetGlobalSiblingIndex(Transform transform)
            {
                var sibling = 0;

                do
                {
                    sibling += transform.GetSiblingIndex() + 1;
                    transform = transform.parent;
                } while (transform != null);

                return sibling - 1;
            }
        }
    }
}