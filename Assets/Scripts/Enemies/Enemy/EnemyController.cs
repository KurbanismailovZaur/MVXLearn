using MVXLearn.Character;
using UnityEngine;
using Zenject;

namespace MVXLearn.Enemies.Enemy
{
    public class EnemyController : ITickable
    {
        [Inject] public EnemyModel Model { get; private set; }
        [Inject] public EnemyView View { get; private set; }

        public EnemyController(EnemyModel model, EnemyView view)
        {
            Model = model;
            View = view;
        }

        public void Tick()
        {
            var transform = View.transform;
            transform.position = Vector3.MoveTowards(transform.position, Model.Character.View.transform.position, Model.Speed * Time.deltaTime);
        }
    }
}