using Azur.WindowsSystem;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindow : Window
    {
        private MenuController _controller;

        [Inject]
        private void InjectMethod(MenuController controller)
        {
            _controller = controller;
        }
    }
}