using _Assets.Scripts.Game.Enemies.Enums;
using _Assets.Scripts.Game.Quests.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Assets.Scripts.Game.Quests.Configs
{
    [CreateAssetMenu(fileName = "QuestTargetTemplate", menuName = "Configs/QuestTargetTemplate")]
    public class QuestTargetTemplate : ScriptableObject
    {
        public EnemyType EnemyType;
        public bool Randomize = false;
        [ShowIf("Randomize")]
        public PropertyToRandomize PropertyToRandomize;
    }
}