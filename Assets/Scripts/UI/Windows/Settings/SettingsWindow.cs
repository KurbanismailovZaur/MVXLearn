using System;
using Azur.WindowsSystem;
using UnityEngine;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsWindow : FadingWindow
    {
        private SettingsModel _model;
        private SettingsView _view;
        private SettingsController _controller;

        [Inject]
        private void InjectMethod(SettingsModel model, SettingsView view, SettingsController controller)
        {
            _model = model;
            _view = view;
            _controller = controller;
        }
        
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            CanvasGroup.alpha = 0f;
        }
    }
}