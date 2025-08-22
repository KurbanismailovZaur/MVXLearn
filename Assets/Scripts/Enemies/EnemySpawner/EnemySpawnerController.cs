using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MVXLearn.Enemies.EnemySpawner
{
    public class EnemySpawnerController : IInitializable
    {
        private float _interval;

        public EnemySpawnerController([Inject(Id = "interval")] float interval)
        {
            _interval = interval;
        }

        public void Initialize() => GenerateEnemiesAsync().Forget();

        private async UniTask GenerateEnemiesAsync()
        {
            while (true)
            {
                await UniTask.WaitForSeconds(_interval);
                // TODO: Create enemy with factory here...
            }
        }
    }
}