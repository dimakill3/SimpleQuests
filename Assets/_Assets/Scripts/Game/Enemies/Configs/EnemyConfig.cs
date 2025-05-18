using _Assets.Scripts.Game.Enemies.Enums;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Assets.Scripts.Game.Enemies.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        public EnemyType EnemyType;
        [MinValue(0)] public int SpawnRate = 1;
        public AssetReference AddressableId;
    }
}