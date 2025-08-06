using MVXLearn.Models;
using MVXLearn.Signals.UI;
using MVXLearn.Views;
using Zenject;

namespace MVXLearn.Controllers
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

            _view.playButton.onClick.AddListener(() => _signalBus.Fire(new PlayClickedSignal()));
            _view.settingsButton.onClick.AddListener(() => _signalBus.Fire(new SettingsOpenClickedSignal()));
        }
    }
}