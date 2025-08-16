using Azur.WindowsSystem;
using MVXLearn.Character;
using MVXLearn.Signals.UI;
using MVXLearn.UI.Animations;
using MVXLearn.UI.Windows.Menu;
using MVXLearn.UI.Windows.Settings;
using UnityEngine;
using Zenject;
using CharacterController = MVXLearn.Character.CharacterController;

namespace MVXLearn.Installers
{
    public class GameInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private FadingWindowActivationAnimation _fadingWindowActivationAnimation;
        [SerializeField] private MeshCollider _groundCollider;
        
        public override void InstallBindings()
        {
            Container.DeclareSignal<PlayClickedSignal>();
            Container.DeclareSignal<SettingsOpenClickedSignal>();
            Container.DeclareSignal<SettingsCloseClickedSignal>();

            Container.Bind<IInitializable>().To<GameController>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<Settings>().AsSingle();

            Container.Bind<WindowsManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<FadingWindowActivationAnimation>().FromInstance(_fadingWindowActivationAnimation);
            
            Container.Bind<UnityEngine.Camera>().FromComponentInHierarchy().AsCached();
            Container.BindInstance(_groundCollider).WithId("ground_collider");
        }
    }
}