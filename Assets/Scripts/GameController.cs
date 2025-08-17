using Azur.WindowsSystem;
using MVXLearn.Enemies.EnemySpawner;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Animations;
using MVXLearn.UI.Windows.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MVXLearn
{
    public class GameController : IInitializable
    {
        private SignalBus _signalBus;

        [Inject]
        private void InjectMethod(SignalBus signalBus) => _signalBus = signalBus;

        public void Initialize() => _signalBus.Subscribe<PlayClickedSignal>(OnPlayClickedSignalHandler);

        private void OnPlayClickedSignalHandler(PlayClickedSignal clickedSignal) => SceneManager.LoadScene("Game");
    }
}