using System;
using System.Collections.Generic;
using _Assets.Scripts.Core.Infrastructure.AssetManagement;
using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Game.Enemies.Configs;
using _Assets.Scripts.Game.Enemies.Enums;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Object = UnityEngine.Object;

namespace _Assets.Scripts.Game.Enemies.Factory
{
    public interface IEnemiesPool : IDisposable
    {
        UniTask<Enemy> Get(EnemyConfig config);
    }

    public class EnemiesPool : IEnemiesPool
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IEnemyFactory _enemyFactory;
        private readonly int _maxSize;
        
        private readonly Dictionary<EnemyType, ObjectPool<Enemy>> _pools = new();
        private readonly Dictionary<EnemyType, Enemy> _prefabs = new();

        [Inject]
        public EnemiesPool(IAssetProvider assetProvider, IEnemyFactory enemyFactory, GameConfig gameConfig)
        {
            _assetProvider = assetProvider;
            _enemyFactory = enemyFactory;
            _maxSize = gameConfig.SpawnConfig.MaxObjectsCount;
        }
        
        public void Dispose()
        {
            foreach (var pool in _pools.Values)
                pool.Dispose();
            
            _pools.Clear();
        }

        public async UniTask<Enemy> Get(EnemyConfig config)
        {
            if (!_pools.TryGetValue(config.EnemyType, out var pool))
                pool = await InitializePool(config);
            
            return pool.Get();
        }

        private async UniTask<ObjectPool<Enemy>> InitializePool(EnemyConfig config)
        {
            var key = config.EnemyType;

            Enemy enemyPrefab;
            
            if (!_prefabs.ContainsKey(key))
            {
                var loadedPrefab = await _assetProvider.Load<GameObject>(config.AddressableId);
                enemyPrefab = loadedPrefab.GetComponent<Enemy>();
                _prefabs[key] = enemyPrefab;
            }
            
            enemyPrefab = _prefabs[key];
            
            var pool = new ObjectPool<Enemy>(
                createFunc: () => CreateInstance(enemyPrefab, config),
                actionOnGet: enemy => enemy.OnSpawned(),
                actionOnRelease: enemy => enemy.OnDespawned(),
                actionOnDestroy: enemy => Despawn(enemy),
                collectionCheck: false,
                defaultCapacity: _maxSize,
                maxSize: _maxSize);

            _pools[config.EnemyType] = pool;
            return pool;
        }

        private Enemy CreateInstance(Enemy enemyPrefab, EnemyConfig enemyConfig)
        {
            var enemy = _enemyFactory.CreateEnemy(enemyPrefab);
            enemy.Initialize(enemyConfig);
            enemy.OnDeath += Release;
            return enemy;
        }

        private void Release(Enemy enemy) =>
            _pools[enemy.EnemyType].Release(enemy);

        private void Despawn(Enemy enemy)
        {
            if (enemy && enemy.gameObject != null)
                enemy.OnDeath -= Release;
            
            Object.Destroy(enemy.gameObject);
        }
    }
}