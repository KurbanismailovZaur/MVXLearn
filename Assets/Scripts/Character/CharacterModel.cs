using UnityEngine;
using Zenject;

namespace MVXLearn.Character
{
    public class CharacterModel
    {
        [Inject(Id = "character_speed")]
        public float CharacterSpeed { get; private set; }

        [Inject(Id = "character_rotate_angle")]
        public float CharacterRotateAngle { get; private set; }

        [Inject]
        public UnityEngine.Camera Camera { get; private set; }

        [Inject(Id = "ground_collider")]
        public MeshCollider GroundCollider { get; private set; }
    }
}