using Azur.WindowsSystem;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Windows.Settings;
using UnityEngine;
using Zenject;

namespace MVXLearn
{
    public class GameController : MonoBehaviour, IInitializable
    {
        private SignalBus _signalBus;
        private WindowsManager _windowsManager;
        private FadingWindowActivationAnimation _fadingWindowActivationAnimation;

        [Inject]
        private void InjectMethod(SignalBus signalBus, WindowsManager windowsManager, FadingWindowActivationAnimation fadingWindowActivationAnimation)
        {
            _signalBus = signalBus;
            _windowsManager = windowsManager;
            _fadingWindowActivationAnimation = fadingWindowActivationAnimation;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayClickedSignal>(OnPlayClickedSignalHandler);
            _signalBus.Subscribe<SettingsOpenClickedSignal>(OnSettingsClickedSignalHandler);
            _signalBus.Subscribe<SettingsCloseClickedSignal>(OnSettingsCloseSignalHandler);
        }

        private void OnPlayClickedSignalHandler(PlayClickedSignal clickedSignal) => print("Game Play Starting!");

        private void OnSettingsClickedSignalHandler() => _windowsManager.ActivateWindow<SettingsView>(windowActivationAnimation: _fadingWindowActivationAnimation);

        private void OnSettingsCloseSignalHandler() => _windowsManager.DeactivateCurrentWindow();
    }
}