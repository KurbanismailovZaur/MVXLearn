using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace MVXLearn.Character
{
    public class CharacterInstaller : Installer<CharacterInstaller>
    {
        [Inject] private CharacterView _characterView;
        private float _speed = 5f;
        private float _rotateAngle = 90f;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterModel>().AsCached().NonLazy();
            Container.BindInstance(_characterView).AsCached();
            Container.Bind<CharacterController>().AsCached().NonLazy();
            
            Container.BindInstance(_speed).WithId("speed");
            Container.BindInstance(_rotateAngle).WithId("rotate_angle");
        }
    }
}