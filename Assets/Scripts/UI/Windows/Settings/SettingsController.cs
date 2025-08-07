using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsController
    {
        private SettingsModel _model;
        private SettingsView _view;
        private SignalBus _signalBus;

        public SettingsController(SettingsModel model, SettingsView view, SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _signalBus = signalBus;
            
            _view.soundButton.onClick.AddListener(OnSoundButtonClickEventHandler);
            _view.vibrationButton.onClick.AddListener(OnVibrationButtonClickEventHandler);
            
            _view.closeButton.onClick.AddListener(OnCloseButtonClickEventHandler);
        }

        private void OnSoundButtonClickEventHandler() => _model.Sound = !_model.Sound;

        private void OnVibrationButtonClickEventHandler() => _model.Vibration = !_model.Vibration;

        private void OnCloseButtonClickEventHandler() => _signalBus.Fire(new SettingsCloseClickedSignal());
    }
}