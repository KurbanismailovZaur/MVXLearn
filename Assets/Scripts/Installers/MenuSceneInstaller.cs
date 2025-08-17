using Azur.WindowsSystem;
using MVXLearn.Character;
using MVXLearn.Enemies.EnemySpawner;
using MVXLearn.Signals.UI;
using MVXLearn.UI;
using MVXLearn.UI.Animations;
using MVXLearn.UI.Windows.Menu;
using MVXLearn.UI.Windows.Settings;
using UnityEngine;
using Zenject;
using CharacterController = MVXLearn.Character.CharacterController;

namespace MVXLearn.Installers
{
    public class MenuSceneInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private FadingWindowActivationAnimation _fadingWindowActivationAnimation;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();

            Container.Bind<Settings>().AsSingle();

            Container.Bind<FadingWindowActivationAnimation>().FromInstance(_fadingWindowActivationAnimation);
            Container.Bind<WindowsManager>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuSceneController>().AsSingle();
        }
    }
}