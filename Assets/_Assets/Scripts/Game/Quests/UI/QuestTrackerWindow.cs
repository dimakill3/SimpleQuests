using System.Collections.Generic;
using _Assets.Scripts.Core.Infrastructure.WindowManagement;
using _Assets.Scripts.Game.Quests.Services;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Game.Quests.UI
{
    public class QuestTrackerWindow : BaseWindow
    {
        [SerializeField] private QuestRow questRowPrefab;
        [SerializeField] private RectTransform container;
        
        private IQuestsProgression _questsProgression;
        private IQuestsGiver _questsGiver;
        private List<QuestRow> _questRows = new ();

        [Inject]
        private void Construct(IQuestsGiver questsGiver) =>
            _questsGiver = questsGiver;

        protected override void Start()
        {
            foreach (var quest in _questsGiver.GetQuests())
            {
                var questRow = Instantiate(questRowPrefab, container, true);
                questRow.Initialize(quest);
                _questRows.Add(questRow);
            }
        }

        protected override void OnDestroy()
        {
            foreach (var questRow in _questRows)
                Destroy(questRow.gameObject);
            
            _questRows.Clear();
        }
    }
}