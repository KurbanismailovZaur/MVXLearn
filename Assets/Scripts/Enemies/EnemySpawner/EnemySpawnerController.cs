using Zenject;

namespace MVXLearn.Enemies.EnemySpawner
{
    public class EnemySpawnerController
    {
        private float _interval;

        public EnemySpawnerController(float interval)
        {
            _interval = interval;
        }
    }
}