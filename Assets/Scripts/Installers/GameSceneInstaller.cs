using MVXLearn.Character;
using MVXLearn.Enemies.EnemySpawner;
using UnityEngine;
using Zenject;
using CharacterController = MVXLearn.Character.CharacterController;

namespace MVXLearn.Installers
{
    public class GameSceneInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private MeshCollider _groundCollider;

        public override void InstallBindings()
        {
            Container.Bind<UnityEngine.Camera>().FromComponentInHierarchy().AsCached();
            Container.BindInstance(_groundCollider).WithId("ground_collider");
            Container.BindInstance(_characterView).WhenInjectedInto<CharacterInstaller>();
            Container.BindInterfacesAndSelfTo<CharacterController>().FromSubContainerResolve().ByInstaller<CharacterInstaller>().AsCached().NonLazy();
        }
    }
}