using MVXLearn.Signals.UI;
using Zenject;

namespace MVXLearn.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayClickedSignal>();

            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        }
    }
}