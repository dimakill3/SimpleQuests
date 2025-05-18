using _Assets.Scripts.Game.Enemies.Configs;
using UnityEngine;

namespace _Assets.Scripts.Game.Configs
{
    [CreateAssetMenu(fileName = "SpawnConfig", menuName = "Configs/SpawnConfig")]
    public class SpawnConfig : ScriptableObject
    {
        public int MaxObjectsCount;
        public EnemyConfig[] EnemiesToSpawn;
    }
}