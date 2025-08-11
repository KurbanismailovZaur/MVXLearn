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
            
            _view.SetButtonsState(_model.Sound, _model.Vibration);
            
            _view.SoundButton.onClick.AddListener(OnSoundButtonClickEventHandler);
            _view.VibrationButton.onClick.AddListener(OnVibrationButtonClickEventHandler);
            
            _model.SettingsStateChanged += OnModelSettingsStateChangedEventHandler;
            
            _view.CloseButton.onClick.AddListener(OnCloseButtonClickEventHandler);
        }

        private void OnSoundButtonClickEventHandler() => _model.Sound = !_model.Sound;

        private void OnVibrationButtonClickEventHandler() => _model.Vibration = !_model.Vibration;

        private void OnModelSettingsStateChangedEventHandler(SettingsModel model) => _view.SetButtonsState(model.Sound, model.Vibration);

        private void OnCloseButtonClickEventHandler() => _signalBus.Fire(new SettingsCloseClickedSignal());
    }
}