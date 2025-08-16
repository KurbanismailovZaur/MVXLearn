using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MVXLearn.UI.Windows.Menu
{
    public class MenuInstaller : MonoInstaller<MenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<MenuModel>().AsCached().NonLazy();
            Container.Bind<MenuView>().FromComponentInHierarchy().AsCached();
            Container.Bind<MenuController>().AsCached().NonLazy();
        }
    }
}