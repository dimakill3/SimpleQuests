using _Assets.Scripts.Game.Quests.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.Game.Quests.UI
{
    public class QuestRow : MonoBehaviour
    {
        [SerializeField] private Image completedBg;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text progressText;

        private QuestConfig _questConfig;
        
        public void Initialize(Quest quest)
        {
            _questConfig = quest.Config;
            
            completedBg.gameObject.SetActive(false);
            description.text = _questConfig.Description;
            UpdateProgress(0, _questConfig.Steps);
            
            quest.OnProgress += UpdateProgress;
            quest.OnCompleted += HandleQuestCompleted;
            
            gameObject.SetActive(true);
        }

        private void UpdateProgress(int currentStep, int steps) =>
            progressText.text = currentStep + "/" + steps;

        private void HandleQuestCompleted() =>
            completedBg.gameObject.SetActive(true);
    }
}