using _Assets.Scripts.Core.Infrastructure.Utils;
using _Assets.Scripts.Game.Configs;
using _Assets.Scripts.Game.Quests.Configs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Assets.Scripts.Core.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [ValueDropdown("GetBuildScenes")]
        public string StartLevelScene;
        public SpawnConfig SpawnConfig;
        public MapConfig MapConfig;
        public QuestsConfig QuestsConfig;

#if UNITY_EDITOR
        private static string[] GetBuildScenes()
        {
            return SceneUtils.GetBuildScenes();
        }
#endif
    }
}