using MVXLearn.Models;
using MVXLearn.Signals.UI;
using MVXLearn.UI;
using UnityEngine;
using Zenject;

namespace MVXLearn.Installers
{
    public class GameInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private MenuWindow _menuWindow;
        [SerializeField] private SettingsWindow _settingsWindow;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayClickedSignal>();
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();
            
            Container.Bind<IInitializable>().To<GameController>().FromInstance(gameController);
            Container.Bind<IInitializable>().To<MenuWindow>().FromInstance(_menuWindow);
            Container.Bind<IInitializable>().To<SettingsWindow>().FromInstance(_settingsWindow);
            
            Container.Bind<SettingsWindow>().FromInstance(_settingsWindow).AsSingle();
            Container.Bind<Settings>().AsSingle();
            Container.Bind<SettingsModel>().AsSingle();
        }
    }
}