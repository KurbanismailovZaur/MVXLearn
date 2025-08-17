using Zenject;

namespace MVXLearn.Enemies.EnemySpawner
{
    public class EnemySpawnerInstaller : Installer<EnemySpawnerInstaller>
    {
        private float _interval = 3f;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemySpawnerController>().AsSingle().NonLazy();
            Container.BindInstance(_interval).WithId("interval");
        }
    }
}