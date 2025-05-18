using System;
using System.Linq;
using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Game.Configs;
using _Assets.Scripts.Game.Enemies.Configs;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Game.Randomizers
{
    public interface IEnemyRandomizer
    {
        EnemyConfig GetRandomEnemy();
    }

    public class EnemyRandomizer : IEnemyRandomizer
    {
        private SpawnConfig _spawnConfig;
        private int _totalSpawnRate;
        private int[] _enemiesSpawnRates;
        
        private EnemyConfig[] _configs;
        
        [Inject]
        public EnemyRandomizer(GameConfig gameConfig)
        {
            _spawnConfig = gameConfig.SpawnConfig;

            CalculateSpawnRates();
        }

        private void CalculateSpawnRates()
        {
            _configs = _spawnConfig.EnemiesToSpawn.Where(c => c != null && c.SpawnRate > 0).ToArray();
            
            if (_configs.Length == 0)
            {
                _enemiesSpawnRates = new int[] {};
                _totalSpawnRate = 0;
                Debug.Log("Where are no enemies to spawn (or all spawn rates equal to 0)");
                return;
            }
            
            _enemiesSpawnRates = new int[_configs.Length];
            int sum = 0;
            for (int i = 0; i < _configs.Length; i++)
            {
                sum += _configs[i].SpawnRate;
                _enemiesSpawnRates[i] = sum;
            }
            _totalSpawnRate = sum;
        }

        public EnemyConfig GetRandomEnemy()
        {
            if (_totalSpawnRate == 0)
            {
                try
                {
                    Debug.Log("You are trying to spawn enemy, but all spawn rates equal to 0. Try return first enemy in SpawnConfig");
                    return _spawnConfig.EnemiesToSpawn[0];
                }
                catch (Exception e)
                {
                    Debug.Log("You are trying to spawn enemy, but where are no enemies to spawn");
                    return null;
                }
            }
            
            var randomValue = UnityEngine.Random.Range(0, _totalSpawnRate);
            
            var idx = Array.BinarySearch(_enemiesSpawnRates, randomValue);
            if (idx < 0)
                idx = ~idx;
            
            return _configs[Mathf.Clamp(idx, 0, _configs.Length - 1)];
        }
    }
}