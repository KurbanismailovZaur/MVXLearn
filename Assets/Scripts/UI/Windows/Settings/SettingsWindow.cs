using Azur.WindowsSystem;
using UnityEngine;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsWindow : FadingWindow
    {
        private SettingsController _controller;

        [Inject]
        private void InjectMethod(SettingsController controller)
        {
            _controller = controller;
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            GetComponent<CanvasGroup>().alpha = 0f;
        }
    }
}