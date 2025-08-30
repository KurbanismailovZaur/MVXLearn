using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindowController : IInitializable
    {
        private SignalBus _signalBus;
        
        public MenuWindowView View { get; private set; }

        public MenuWindowController(MenuWindowView view, SignalBus signalBus)
        {
            View = view;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            View.PlayButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            View.SettingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}