using _Assets.Scripts.Core.Infrastructure.Utils;
using _Assets.Scripts.Game.Enemies.Enums;
using _Assets.Scripts.Game.Quests.Configs;
using _Assets.Scripts.Game.Quests.Enums;
using UnityEngine;

namespace _Assets.Scripts.Game.Quests.Utils
{
    public static class QuestUtils
    {
        public static QuestConfig RandomizeQuest(QuestConfig questConfig)
        {
            var randomizedQuest = ScriptableObject.CreateInstance<QuestConfig>();
            
            var json = JsonUtility.ToJson(questConfig);
            JsonUtility.FromJsonOverwrite(json, randomizedQuest);
            
            switch (questConfig.TargetTemplate.PropertyToRandomize)
            {
                case PropertyToRandomize.EnemyType:
                    randomizedQuest.TargetTemplate.EnemyType = EnumUtils.GetRandomEnumValue(EnemyType.None);
                    randomizedQuest.Description = randomizedQuest.Description.Replace("{}", randomizedQuest.TargetTemplate.EnemyType.ToString());
                    break;
            }

            return randomizedQuest;
        }
    }
}