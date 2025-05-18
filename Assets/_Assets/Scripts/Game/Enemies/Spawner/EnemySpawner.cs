using System;
using System.Threading;
using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Game.Configs;
using _Assets.Scripts.Game.Enemies.Configs;
using _Assets.Scripts.Game.Enemies.Factory;
using _Assets.Scripts.Game.Map;
using _Assets.Scripts.Game.Randomizers;
using Cysharp.Threading.Tasks;

namespace _Assets.Scripts.Game.Enemies.Spawner
{
    public interface IEnemySpawner
    {
        public void StartSpawn();
        public void StopSpawn();
    }

    public class EnemySpawner : IEnemySpawner, IDisposable
    {
        private readonly GameConfig _gameConfig;
        private readonly SpawnConfig _spawnConfig;
        private readonly IEnemiesPool _enemiesPool;
        private readonly IEnemyRandomizer _enemyRandomizer;

        private int _currentEnemiesCount;
        private CancellationTokenSource _spawnCts;

        public EnemySpawner(GameConfig gameConfig, IEnemiesPool enemiesPool, IEnemyRandomizer enemyRandomizer)
        {
            _gameConfig = gameConfig;
            _spawnConfig = gameConfig.SpawnConfig;
            _enemiesPool = enemiesPool;
            _enemyRandomizer = enemyRandomizer;
        }

        public void StartSpawn()
        {
            if (_spawnCts != null)
                return;
            _spawnCts = new CancellationTokenSource();
            SpawnLoop(_spawnCts.Token).Forget();
        }

        public void StopSpawn()
        {
            _spawnCts?.Cancel();
            _spawnCts = null;
        }

        public void Dispose()
        {
            _enemiesPool?.Dispose();
            _spawnCts?.Dispose();
        }

        private async UniTaskVoid SpawnLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (_currentEnemiesCount >= _spawnConfig.MaxObjectsCount)
                    continue;
                
                SpawnEnemy().Forget();
                _currentEnemiesCount++;
                
                await UniTask.WaitForFixedUpdate();
            }
        }

        private async UniTaskVoid SpawnEnemy()
        {
            EnemyConfig randomEnemy = _enemyRandomizer.GetRandomEnemy();
            var spawnPosition = MapUtil.GetRandomMapPosition();

            var enemy = await _enemiesPool.Get(randomEnemy);
            enemy.transform.position = spawnPosition;

            enemy.OnDeath += HandleEnemyDeath;
        }

        private void HandleEnemyDeath(Enemy enemy)
        {
            enemy.OnDeath -= HandleEnemyDeath;
            _currentEnemiesCount--;
        }
    }
}