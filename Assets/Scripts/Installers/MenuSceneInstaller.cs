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
        [SerializeField] private WindowsManager _windowsManager;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();

            Container.Bind<Settings>().AsSingle();
            Container.BindInstance(_windowsManager);
            Container.BindInterfacesAndSelfTo<MenuSceneController>().AsSingle();

            Container.Bind<MenuWindowController>().FromSubContainerResolve().ByInstaller<MenuWindowInstaller>().AsCached().NonLazy();
        }
    }
}