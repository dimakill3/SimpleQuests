using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Game.Enemies.Factory
{
    public interface IEnemyFactory
    {
        Enemy CreateEnemy(Enemy enemyPrefab, Vector3 at = new());
    }

    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;

        [Inject]
        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public Enemy CreateEnemy(Enemy enemyPrefab, Vector3 at = new())
        {
            var enemy = _diContainer.InstantiatePrefabForComponent<Enemy>(enemyPrefab, at, Quaternion.identity, null);

            return enemy;
        }
    }
}