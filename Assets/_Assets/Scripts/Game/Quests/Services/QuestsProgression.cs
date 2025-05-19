using System;
using System.Linq;
using _Assets.Scripts.Core.Infrastructure.EventManagement;
using _Assets.Scripts.Game.Enemies.Enums;
using _Assets.Scripts.Game.Events;
using _Assets.Scripts.Game.Quests.Enums;
using _Assets.Scripts.Game.Time;
using Zenject;

namespace _Assets.Scripts.Game.Quests.Services
{
    public interface IQuestsProgression : IDisposable
    {
        event Action AllQuestsCompleted;
        
        void StartProgression();
    }

    public class QuestsProgression : IQuestsProgression
    {
        public event Action AllQuestsCompleted;
        
        private readonly IQuestsGiver _questsGiver;
        private readonly IEventProvider _eventProvider;
        private readonly IPlayTimeService _playTimeService;

        private Quest[] _quests;


        [Inject]
        public QuestsProgression(IQuestsGiver questsGiver, IEventProvider eventProvider, IPlayTimeService playTimeService)
        {
            _questsGiver = questsGiver;
            _eventProvider = eventProvider;
            _playTimeService = playTimeService;
        }

        public void StartProgression()
        {
            _quests = _questsGiver.GetQuests();
            
            _eventProvider.Subscribe<EnemyDestroyedEvent>(HandleEnemyDestroyed);
            _playTimeService.OnSecondsChanged += HandleSecondChanged;

            foreach (var quest in _quests)
                quest.OnCompleted += HandleQuestComplete;
        }

        private void HandleQuestComplete()
        {
            if (_quests.All(q => q.IsCompleted))
                AllQuestsCompleted?.Invoke();
        }

        public void Dispose()
        {
            foreach (var quest in _quests)
                quest.OnCompleted -= HandleQuestComplete;
            
            _eventProvider.UnSubscribe<EnemyDestroyedEvent>(HandleEnemyDestroyed);
            _playTimeService.OnSecondsChanged -= HandleSecondChanged;
        }

        private void HandleEnemyDestroyed(EnemyDestroyedEvent enemyDestroyedEvent)
        {
            foreach (var quest in _quests)
            {
                if (quest.Config.QuestType != QuestType.DestroyXEnemies)
                    continue;
                
                if (quest.Config.TargetTemplate == null)
                    continue;

                if (quest.Config.TargetTemplate.EnemyType == EnemyType.None || quest.Config.TargetTemplate.EnemyType == enemyDestroyedEvent.EnemyType)
                    quest.CompleteStep();
            }
        }

        private void HandleSecondChanged(int seconds)
        {
            foreach (var quest in _quests)
            {
                if (quest.Config.QuestType != QuestType.PlayXSeconds || quest.IsCompleted)
                    continue;

                quest.CompleteStep();
            }
        }
    }
}