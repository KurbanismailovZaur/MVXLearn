using MVXLearn.Configs;
using MVXLearn.Persistence;
using MVXLearn.Signals.UI;
using UnityEngine;
using Zenject;

namespace MVXLearn.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private GameConfig _gameConfig;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayClickedSignal>();

            Container.BindInstance(_gameConfig);    
            Container.BindInterfacesAndSelfTo<SettingsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }
    }
}