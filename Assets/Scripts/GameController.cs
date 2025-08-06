using MVXLearn.Models;
using MVXLearn.Signals.UI;
using MVXLearn.UI;
using UnityEngine;
using Zenject;

namespace MVXLearn
{
    public class GameController : MonoBehaviour, IInitializable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private SettingsWindow _settingsWindow;

        public void Initialize()
        {
            _signalBus.Subscribe<PlayClickedSignal>(OnPlayClickedSignalHandler);
            _signalBus.Subscribe<SettingsOpenClickedSignal>(OnSettingsClickedSignalHandler);
            _signalBus.Subscribe<SettingsCloseClickedSignal>(OnSettingsCloseSignalHandler);
        }

        private void OnPlayClickedSignalHandler(PlayClickedSignal clickedSignal) => print("Game Play Starting!");

        private void OnSettingsClickedSignalHandler() => _settingsWindow.gameObject.SetActive(true);

        private void OnSettingsCloseSignalHandler() => _settingsWindow.gameObject.SetActive(false);
    }
}