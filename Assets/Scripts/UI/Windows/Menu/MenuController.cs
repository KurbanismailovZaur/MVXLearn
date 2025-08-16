using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuController
    {
        private SignalBus _signalBus;
        
        public MenuModel Model { get; private set; }
        public MenuView View { get; private set; }

        public MenuController(SignalBus signalBus, MenuModel model, MenuView view)
        {
            _signalBus = signalBus;
            Model = model;
            View = view;

            View.PlayButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            View.SettingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}