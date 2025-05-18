using System;
using _Assets.Scripts.Core.Infrastructure.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Assets.Scripts.Game.Quests.Configs
{
    [Serializable]
    public class LevelQuestsData
    {
        [ValueDropdown("GetBuildScenes")]
        public String level;
        public QuestConfig[] quests = {};
        
#if UNITY_EDITOR
        private static string[] GetBuildScenes()
        {
            return SceneUtils.GetBuildScenes();
        }
#endif
    }

    [CreateAssetMenu(fileName = "QuestsConfig", menuName = "Configs/QuestsConfig")]
    public class QuestsConfig : ScriptableObject
    {
        public LevelQuestsData[] LevelQuests = {};
    }
}