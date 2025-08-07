using Azur.WindowsSystem;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Windows.Menu;
using MVXLearn.UI.Windows.Settings;
using UnityEngine;
using Zenject;

namespace MVXLearn.Installers
{
    public class GameInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private FadingWindowActivationAnimation _fadingWindowActivationAnimation;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayClickedSignal>();
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();

            Container.Bind<IInitializable>().To<GameController>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<Settings>().AsSingle();

            Container.Bind<WindowsManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MenuWindow>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SettingsWindow>().FromComponentInHierarchy().AsSingle();

            Container.Bind<FadingWindowActivationAnimation>().FromInstance(_fadingWindowActivationAnimation);
        }
    }
}