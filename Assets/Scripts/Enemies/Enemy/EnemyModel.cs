using UnityEngine;
using Zenject;

namespace MVXLearn.Enemies.Enemy
{
    public class EnemyModel
    {
        [Inject]
        public MVXLearn.Character.CharacterController Character { get; private set; }
        
        [Inject(Id = "speed")]
        public float Speed { get; private set; }
    }
}