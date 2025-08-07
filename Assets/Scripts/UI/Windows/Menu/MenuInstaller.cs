using UnityEngine;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuInstaller : MonoInstaller<MenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuModel>().AsSingle();
            Container.Bind<MenuView>().FromComponentInHierarchy().AsCached();
            Container.Bind<MenuController>().AsCached();
        }
    }
}