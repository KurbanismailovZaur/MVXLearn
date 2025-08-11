using Azur.WindowsSystem;
using UnityEditor;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindow : Window
    {
        private MenuModel _model;
        private MenuView _view;
        private MenuController _controller;

        [Inject]
        private void InjectMethod(MenuModel model, MenuView view, MenuController controller)
        {
            _model = model;
            _view = view;
            _controller = controller;
        }
    }
}