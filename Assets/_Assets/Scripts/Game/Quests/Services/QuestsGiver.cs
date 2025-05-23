﻿using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Core.Infrastructure.Utils;
using _Assets.Scripts.Game.Enemies.Enums;
using _Assets.Scripts.Game.Quests.Configs;
using _Assets.Scripts.Game.Quests.Enums;
using _Assets.Scripts.Game.Quests.Utils;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Game.Quests.Services
{
    public interface IQuestsGiver
    {
        Quest[] GetQuests();
    }

    public class QuestsGiver : IQuestsGiver
    {
        private QuestsConfig _questsConfig;
        private GameConfig _gameConfig;
        private Quest[] _currentQuests;

        [Inject]
        public QuestsGiver(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _questsConfig = gameConfig.QuestsConfig;
        }

        public Quest[] GetQuests()
        {
            if (_currentQuests == null || _currentQuests.Length == 0)
            {
                QuestConfig[] questsToGive = null;
                for (var i = 0; i < _questsConfig.LevelQuests.Length; i++)
                    if (_questsConfig.LevelQuests[i].level.Equals(_gameConfig.StartLevelScene))
                        questsToGive = _questsConfig.LevelQuests[i].quests;

                if (questsToGive == null)
                    return new Quest[] { };

                var quests = new Quest[questsToGive.Length];
                for (var i = 0; i < questsToGive.Length; i++)
                {
                    var quest = questsToGive[i];    
                    if (quest.TargetTemplate != null && quest.TargetTemplate.Randomize)
                        quest = QuestUtils.RandomizeQuest(quest);

                    quests[i] = new Quest(quest);
                }

                _currentQuests = quests;
                return quests;
            }

            return _currentQuests;
        }
    }
}