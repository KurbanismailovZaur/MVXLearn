using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuController
    {
        private readonly MenuModel _model;
        private readonly MenuView _view;
        private readonly SignalBus _signalBus;

        public MenuController(MenuModel model, MenuView view, SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _signalBus = signalBus;

            _view.PlayButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            _view.SettingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}