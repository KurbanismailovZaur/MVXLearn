using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindowController
    {
        private SignalBus _signalBus;
        
        public MenuWindowModel WindowModel { get; private set; }
        public MenuWindowView WindowView { get; private set; }

        public MenuWindowController(SignalBus signalBus, MenuWindowModel windowModel, MenuWindowView windowView)
        {
            _signalBus = signalBus;
            WindowModel = windowModel;
            WindowView = windowView;

            WindowView.PlayButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            WindowView.SettingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}