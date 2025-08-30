using Azur.WindowsSystem;
using MVXLearn.Extensions;
using MVXLearn.Persistence;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Windows.Menu;
using MVXLearn.UI.Windows.Settings;
using UnityEngine;
using Zenject;

namespace MVXLearn.Installers
{
    public class MenuSceneInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private WindowsManager _windowsManager;
        
        [SerializeField] private GameObjectContext _menuWindowContext;
        [SerializeField] private GameObjectContext _settingsWindowContext;

        public override void InstallBindings()
        {
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();

            Container.BindInstance(_windowsManager);
            Container.BindInterfacesAndSelfTo<MenuSceneController>().AsSingle();

            Container.BindFromGameObjectContext<MenuWindowController>(_menuWindowContext).AsCached().NonLazy();
            Container.BindFromGameObjectContext<SettingsWindowController>(_settingsWindowContext).AsCached().NonLazy();
        }
    }
}