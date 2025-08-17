using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsWindowController
    {
        private SignalBus _signalBus;
        
        public SettingsWindowModel WindowModel { get; private set; }
        public SettingsWindowView WindowView { get; private set; }

        public SettingsWindowController(SignalBus signalBus, SettingsWindowModel windowModel, SettingsWindowView windowView)
        {
            _signalBus = signalBus;
            WindowModel = windowModel;
            WindowView = windowView;
            
            WindowView.SetButtonsState(WindowModel.Sound, WindowModel.Vibration);
            
            WindowView.SoundButton.onClick.AddListener(OnSoundButtonClickEventHandler);
            WindowView.VibrationButton.onClick.AddListener(OnVibrationButtonClickEventHandler);
            
            WindowModel.SettingsStateChanged += OnWindowModelSettingsStateChangedEventHandler;
            
            WindowView.CloseButton.onClick.AddListener(OnCloseButtonClickEventHandler);
            WindowView.Deactivating += _ => WindowView.CanvasGroup.alpha = 0f;
        }

        private void OnSoundButtonClickEventHandler() => WindowModel.Sound = !WindowModel.Sound;

        private void OnVibrationButtonClickEventHandler() => WindowModel.Vibration = !WindowModel.Vibration;

        private void OnWindowModelSettingsStateChangedEventHandler(SettingsWindowModel windowModel) => WindowView.SetButtonsState(windowModel.Sound, windowModel.Vibration);

        private void OnCloseButtonClickEventHandler() => _signalBus.Fire(new SettingsCloseClickedSignal());
    }
}