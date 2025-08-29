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
        
        [SerializeField] private GameObjectContext _menuWindowContext;
        [SerializeField] private GameObjectContext _settingsWindowContext;

        public override void InstallBindings()
        {
            _menuWindowContext.Install(Container);
            _settingsWindowContext.Install(Container);
            
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();

            Container.Bind<Settings>().AsSingle();
            Container.BindInstance(_windowsManager);
            Container.BindInterfacesAndSelfTo<MenuSceneController>().AsSingle();

            Container.Bind<MenuWindowController>().FromSubContainerResolve().ByInstance(_menuWindowContext.Container).AsCached().NonLazy();
            Container.Bind<SettingsWindowController>().FromSubContainerResolve().ByInstance(_settingsWindowContext.Container).AsCached().NonLazy();
        }
    }
}