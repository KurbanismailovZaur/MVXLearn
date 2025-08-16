using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace MVXLearn.Character
{
    public class CharacterInstaller : MonoInstaller<CharacterInstaller>
    {
        [SerializeField] private float _characterSpeed = 5f;
        [SerializeField] private float _characterRotateAngle = 90f;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterModel>().AsCached().NonLazy();
            Container.Bind<CharacterView>().FromComponentInHierarchy().AsCached();
            Container.BindInterfacesAndSelfTo<CharacterController>().AsCached().NonLazy();

            Container.BindInstance(_characterSpeed).WithId("character_speed");
            Container.BindInstance(_characterRotateAngle).WithId("character_rotate_angle");
        }
    }
}