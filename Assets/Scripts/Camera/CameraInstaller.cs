using UnityEngine;
using Zenject;

namespace MVXLearn.Camera
{
    public class CameraInstaller : MonoInstaller<CameraInstaller>
    {
        [SerializeField] private float _height;
        [SerializeField] private float _interpolation;
        [SerializeField] private Transform _target;
        
        public override void InstallBindings()
        {
            Container.Bind<CameraModel>().AsCached().NonLazy();
            Container.Bind<CameraView>().FromComponentInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<CameraController>().AsCached().NonLazy();

            Container.BindInstance(_height).WithId("height");
            Container.BindInstance(_interpolation).WithId("interpolation");
            Container.BindInstance(_target).WithId("target");
        }
    }
}