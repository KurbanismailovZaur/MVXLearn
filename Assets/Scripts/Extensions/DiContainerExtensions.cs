using MVXLearn.UI.Windows.Menu;
using Zenject;

namespace MVXLearn.Extensions
{
    public static class DiContainerExtensions
    {
        public static ScopeConcreteIdArgConditionCopyNonLazyBinder BindFromGameObjectContext<T>(this DiContainer container, GameObjectContext context)
        {
            context.Install(container);
            return container.BindInterfacesAndSelfTo<T>().FromSubContainerResolve().ByInstance(context.Container);
        }
    }
}