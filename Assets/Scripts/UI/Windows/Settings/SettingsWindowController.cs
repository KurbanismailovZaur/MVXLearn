using MVXLearn.Persistence;
using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Settings
{
    public class SettingsWindowController : IInitializable
    {
        private SignalBus _signalBus;

        private SettingsService _settingsService;
        
        public SettingsWindowView View { get; private set; }

        public SettingsWindowController(SettingsWindowView view, SignalBus signalBus, SettingsService settingsService)
        {
            View = view;
            _signalBus = signalBus;
            _settingsService = settingsService;
        }

        public void Initialize()
        {
            View.SetButtonsState(_settingsService.Sound, _settingsService.Vibration);
            
            View.SoundButton.onClick.AddListener(OnSettingsService_SoundButtonOnClickEventHandler);
            View.VibrationButton.onClick.AddListener(OnSettingsService_VibrationButtonOnClickEventHandler);
            
            _settingsService.SettingsDataChanged += OnSettingsService_SettingsDataChangedEventHandler;
            
            View.CloseButton.onClick.AddListener(OnCloseButtonClickEventHandler);
        }

        private void OnSettingsService_SoundButtonOnClickEventHandler()
        {
            _settingsService.Sound = !_settingsService.Sound;
        }

        private void OnSettingsService_VibrationButtonOnClickEventHandler()
        {
            _settingsService.Vibration = !_settingsService.Vibration;
        }

        private void OnSettingsService_SettingsDataChangedEventHandler(SettingsService settingsService)
        {
            View.SetButtonsState(_settingsService.Sound, _settingsService.Vibration);
        }

        private void OnCloseButtonClickEventHandler() => _signalBus.Fire(new SettingsCloseClickedSignal());
    }
}