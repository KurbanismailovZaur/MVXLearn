using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindowController
    {
        private SignalBus _signalBus;
        
        public MenuWindowView View { get; private set; }

        public MenuWindowController(SignalBus signalBus, MenuWindowView view)
        {
            _signalBus = signalBus;
            View = view;

            View.PlayButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            View.SettingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}