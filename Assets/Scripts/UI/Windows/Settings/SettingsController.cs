using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsController
    {
        private SignalBus _signalBus;
        
        public SettingsModel Model { get; private set; }
        public SettingsView View { get; private set; }

        public SettingsController(SignalBus signalBus, SettingsModel model, SettingsView view)
        {
            _signalBus = signalBus;
            Model = model;
            View = view;
            
            View.SetButtonsState(Model.Sound, Model.Vibration);
            
            View.SoundButton.onClick.AddListener(OnSoundButtonClickEventHandler);
            View.VibrationButton.onClick.AddListener(OnVibrationButtonClickEventHandler);
            
            Model.SettingsStateChanged += OnModelSettingsStateChangedEventHandler;
            
            View.CloseButton.onClick.AddListener(OnCloseButtonClickEventHandler);
            View.Deactivating += _ => View.CanvasGroup.alpha = 0f;
        }

        private void OnSoundButtonClickEventHandler() => Model.Sound = !Model.Sound;

        private void OnVibrationButtonClickEventHandler() => Model.Vibration = !Model.Vibration;

        private void OnModelSettingsStateChangedEventHandler(SettingsModel model) => View.SetButtonsState(model.Sound, model.Vibration);

        private void OnCloseButtonClickEventHandler() => _signalBus.Fire(new SettingsCloseClickedSignal());
    }
}