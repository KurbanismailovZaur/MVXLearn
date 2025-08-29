using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuWindowInstaller : Installer<MenuWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuWindowView>().FromComponentInHierarchy().AsCached();
            Container.Bind<MenuWindowController>().AsCached();
        }
    }
}