using MVXLearn.Enemies.EnemySpawner;
using UnityEngine;
using Zenject;

namespace MVXLearn.Installers
{
    public class GameSceneInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private MeshCollider _groundCollider;

        public override void InstallBindings()
        {
            Container.Bind<UnityEngine.Camera>().FromComponentInHierarchy().AsCached();
            Container.BindInstance(_groundCollider).WithId("ground_collider");

            Container.Bind<EnemySpawnerController>().FromSubContainerResolve().ByInstaller<EnemySpawnerInstaller>().AsCached();
        }
    }
}