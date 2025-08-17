using Azur.WindowsSystem;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Animations;
using MVXLearn.UI.Windows.Settings;
using Zenject;

namespace MVXLearn
{
    public class MenuSceneController : IInitializable
    {
        private WindowsManager _windowsManager;
        private SignalBus _signalBus;
        private FadingWindowActivationAnimation _fadingWindowActivationAnimation;
        
        public MenuSceneController(SignalBus signalBus, WindowsManager windowsManager,
            FadingWindowActivationAnimation fadingWindowActivationAnimation)
        {
            _signalBus = signalBus;
            _windowsManager = windowsManager;
            _fadingWindowActivationAnimation = fadingWindowActivationAnimation;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SettingsOpenClickedSignal>(OnSettingsOpenClickedSignalHandler);
            _signalBus.Subscribe<SettingsCloseClickedSignal>(OnSettingsCloseClickedSignalHandler);
        }

        private void OnSettingsOpenClickedSignalHandler()
        {
            _windowsManager.ActivateWindow<SettingsWindowView>(windowActivationAnimation : _fadingWindowActivationAnimation);
        }
        
        private void OnSettingsCloseClickedSignalHandler()
        {
            _windowsManager.DeactivateCurrentWindow();
        }
    }
}