using System;
using System.Collections;
using Azur.WindowsSystem.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Azur.WindowsSystem
{
    /// <summary>
    /// A base class representing a window. You can use it directly or inherit from it to create other classes.
    /// It can activate and deactivate windows.
    /// </summary>
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class Window : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected GraphicRaycaster _graphicRaycaster;
        [SerializeField] protected WindowActivationAnimation defaultWindowActivationAnimation;
        [SerializeField] protected WindowDeactivationAnimation defaultWindowDeactivationAnimation;

        protected bool _isInTransition;
        
        /// <summary>
        /// It will return <c>true</c> if the window is currently being activated or deactivated.
        /// </summary>
        public bool IsInTransition => _isInTransition;

        /// <summary>
        /// A reference to the window's canvas.
        /// </summary>
        public Canvas Canvas => _canvas;
        
        /// <summary>
        /// A reference to the window's graphic raycaster.
        /// </summary>
        public GraphicRaycaster GraphicRaycaster => _graphicRaycaster;
        
        /// <summary>
        /// An activation event that will trigger before the activation animation starts, right before the window's game object becomes active.
        /// </summary>
        public Action<Window> Activating;
        
        /// <summary>
        /// An activation event that triggers after the activation animation is complete.
        /// </summary>
        public Action<Window> Activated;
        
        /// <summary>
        /// A deactivation event that triggers right before the deactivation animation starts.
        /// </summary>
        public Action<Window> Deactivating;
        
        /// <summary>
        /// A deactivation event that triggers after the deactivation animation is complete.
        /// </summary>
        public Action<Window> Deactivated;

        protected virtual void Reset()
        {
            _canvas = GetComponent<Canvas>();
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            
            _canvas.overrideSorting = true;
        }

        protected virtual void OnInstantiated() { }

        /// <summary>
        /// Activates the window with default parameters.
        /// </summary>
        public void Activate() => Activate(true, null, null);
        
        /// <summary>
        /// Activates the window. Allows specifying the time scale parameters, activation animation, and the owner of the activation coroutine.
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowActivationAnimation">Window activation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// <param name="coroutineOwner">Who should run the window activation coroutine? If <c>null</c>, the owner of the coroutine will be the window itself.</param>
        /// </summary>
        public virtual Coroutine Activate(bool useUnscaledTime = true, WindowActivationAnimation windowActivationAnimation = null, MonoBehaviour coroutineOwner = null)
        {
            if (gameObject.activeSelf)
                throw new InvalidOperationException("Window is already active.");

            if (_isInTransition)
                throw new InvalidOperationException("Window is already in transition state.");
            
            _isInTransition = true;

            OnActivating();
            Activating?.Invoke(this);
            gameObject.SetActive(true);

            return (coroutineOwner ?? this).StartCoroutine(ActivateEnumerator(windowActivationAnimation ?? defaultWindowActivationAnimation, useUnscaledTime));
        }

        protected virtual IEnumerator ActivateEnumerator(WindowActivationAnimation windowActivationAnimation, bool useUnscaledTime)
        {
            var animateEnumerator = windowActivationAnimation.AnimateEnumerator(this, useUnscaledTime);

            while (animateEnumerator.MoveNext())
                yield return animateEnumerator.Current;
            
            _isInTransition = false;
            OnActivated();
            Activated?.Invoke(this);
        }
        
        protected virtual void OnActivating() { }
        
        protected virtual void OnActivated() { }

        /// <summary>
        /// Deactivates the window with default parameters.
        /// </summary>
        public void Deactivate() => Deactivate(true, null, null);

        /// <summary>
        /// Deactivates the window. Allows specifying the time scale parameters, deactivation animation, and the owner of the deactivation coroutine.
        /// <param name="useUnscaledTime">Should <c>Time.timeScale</c> be ignored?</param>
        /// <param name="windowDeactivationAnimation">Window deactivation animation. If <c>null</c>, the default animation specified in the inspector is used.</param>
        /// <param name="coroutineOwner">Who should run the window deactivation coroutine? If <c>null</c>, the owner of the coroutine will be the window itself.</param>
        /// </summary>
        public virtual Coroutine Deactivate(bool useUnscaledTime = false, WindowDeactivationAnimation windowDeactivationAnimation = null, MonoBehaviour coroutineOwner = null)
        {
            if (!gameObject.activeSelf)
                throw new InvalidOperationException("Window is already deactivated.");

            if (_isInTransition)
                throw new InvalidOperationException("Window is already in transition state.");

            _isInTransition = true;
            OnDeactivating();
            Deactivating?.Invoke(this);
            
            return (coroutineOwner ?? this).StartCoroutine(DeactivateEnumerator(windowDeactivationAnimation ?? defaultWindowDeactivationAnimation, useUnscaledTime));
        }

        protected virtual IEnumerator DeactivateEnumerator(WindowDeactivationAnimation windowDeactivationAnimation, bool useUnscaledTime)
        {
            var animateEnumerator = windowDeactivationAnimation.AnimateEnumerator(this, useUnscaledTime);

            while (animateEnumerator.MoveNext())
                yield return animateEnumerator.Current;

            _isInTransition = false;
            gameObject.SetActive(false);
            OnDeactivated();
            Deactivated?.Invoke(this);
        }
        
        protected virtual void OnDeactivating() { }
        
        protected virtual void OnDeactivated() { }
        
        internal static class ProtectedMembersAccessor
        {
            internal static void CallOnInstantiated(Window window) => window.OnInstantiated();
        }
    }
}