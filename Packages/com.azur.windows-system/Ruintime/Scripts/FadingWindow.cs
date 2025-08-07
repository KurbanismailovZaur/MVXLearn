using UnityEngine;

namespace Azur.WindowsSystem
{
    /// <summary>
    /// Represents a window with a CanvasGroup component. Automates blocking of interactive elements of the window during activation/deactivation.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class FadingWindow : Window
    {
        [SerializeField] protected CanvasGroup _canvasGroup;

        /// <summary>
        /// A reference to the window's canvas group.
        /// </summary>
        public CanvasGroup CanvasGroup => _canvasGroup;

        protected override void Reset()
        {
            base.Reset();
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.blocksRaycasts = false;
        }

        protected override void OnActivating() => _canvasGroup.blocksRaycasts = false;

        protected override void OnActivated() => _canvasGroup.blocksRaycasts = true;
        
        protected override void OnDeactivating() => _canvasGroup.blocksRaycasts = false;

        protected override void OnDeactivated() => _canvasGroup.blocksRaycasts = true;

    }
}