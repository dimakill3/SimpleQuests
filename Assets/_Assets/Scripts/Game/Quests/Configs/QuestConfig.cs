using _Assets.Scripts.Game.Quests.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Assets.Scripts.Game.Quests.Configs
{
    [CreateAssetMenu(fileName = "QuestConfig", menuName = "Configs/QuestConfig")]
    public class QuestConfig : ScriptableObject
    {
        public QuestType QuestType;
        public string Description;
        [MinValue(1)]
        public int Steps;
        public QuestTargetTemplate TargetTemplate;
    }
}