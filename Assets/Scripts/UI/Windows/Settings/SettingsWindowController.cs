using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsWindowController
    {
        private SignalBus _signalBus;
        
        public SettingsWindowModel Model { get; private set; }
        public SettingsWindowView View { get; private set; }

        public SettingsWindowController(SignalBus signalBus, SettingsWindowModel model, SettingsWindowView view)
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

        private void OnModelSettingsStateChangedEventHandler(SettingsWindowModel windowModel) => View.SetButtonsState(windowModel.Sound, windowModel.Vibration);

        private void OnCloseButtonClickEventHandler() => _signalBus.Fire(new SettingsCloseClickedSignal());
    }
}