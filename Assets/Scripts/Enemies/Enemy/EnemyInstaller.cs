using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace MVXLearn.Enemies.Enemy
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private float _speed = 5f;
        
        public override void InstallBindings()
        {
            Container.Bind<EnemyModel>().AsCached().NonLazy();
            Container.Bind<EnemyView>().FromComponentInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<EnemyController>().AsCached().NonLazy();

            Container.BindInstance(_speed).WithId("speed");
        }

    }
}