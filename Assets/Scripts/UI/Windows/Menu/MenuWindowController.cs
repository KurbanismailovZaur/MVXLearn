using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindowController
    {
        private SignalBus _signalBus;
        
        public MenuWindowModel Model { get; private set; }
        public MenuWindowView View { get; private set; }

        public MenuWindowController(SignalBus signalBus, MenuWindowModel model, MenuWindowView view)
        {
            _signalBus = signalBus;
            Model = model;
            View = view;

            View.PlayButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            View.SettingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}