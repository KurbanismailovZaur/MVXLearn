using Azur.WindowsSystem;
using MVXLearn.Persistence;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Animations;
using MVXLearn.UI.Windows.Settings;
using Zenject;

namespace MVXLearn
{
    public class MenuSceneController : IInitializable
    {
        [Inject] private WindowsManager _windowsManager;
        [Inject] private SignalBus _signalBus;
        [Inject] private SettingsService _settingsService;
        
        public void Initialize()
        {
            _signalBus.Subscribe<SettingsOpenClickedSignal>(OnSettingsOpenClickedSignalHandler);
            _signalBus.Subscribe<SettingsCloseClickedSignal>(OnSettingsCloseClickedSignalHandler);
        }

        private void OnSettingsOpenClickedSignalHandler() => _windowsManager.ActivateWindow<SettingsWindowView>();

        private void OnSettingsCloseClickedSignalHandler()
        {
            _windowsManager.DeactivateCurrentWindow();
            _settingsService.SaveToFile();
        }
    }
}