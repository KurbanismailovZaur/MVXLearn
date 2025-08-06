using MVXLearn.Models;
using MVXLearn.Signals.UI;
using MVXLearn.Views;
using Zenject;

namespace MVXLearn.Controllers
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
            
            _view.soundButton.onClick.AddListener(OnSoundButtonClickEventHandler);
            _view.vibrationButton.onClick.AddListener(OnVibrationButtonClickEventHandler);
            
            _model.SettingsStateChanged += OnModelSettingsStateChangedEventHandler;
            
            _view.closeButton.onClick.AddListener(OnCloseButtonClickEventHandler);
        }

        private void OnSoundButtonClickEventHandler() => _model.Sound = !_model.Sound;

        private void OnVibrationButtonClickEventHandler() => _model.Vibration = !_model.Vibration;

        private void OnModelSettingsStateChangedEventHandler(SettingsModel model)
        {
            _view.SetButtonsState(model.Sound, model.Vibration);
        }

        private void OnCloseButtonClickEventHandler()
        {
            _signalBus.Fire(new SettingsCloseClickedSignal());
        }
    }
}