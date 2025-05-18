using System;
using _Assets.Scripts.Game.Quests.Configs;

namespace _Assets.Scripts.Game.Quests
{
    public class Quest : IDisposable
    {
        public event Action OnCompleted;
        public event Action<int, int> OnProgress;

        public QuestConfig Config { get; private set; }
        public bool IsCompleted { get; private set; } = false;

        private int _currentStep = 0;

        public Quest(QuestConfig config)
        {
            Config = config;
        }

        public void CompleteStep()
        {
            if (IsCompleted)
                return;

            _currentStep++;
            OnProgress?.Invoke(_currentStep, Config.Steps);

            if (_currentStep < Config.Steps)
                return;

            IsCompleted = true;
            OnCompleted?.Invoke();
        }

        public void Dispose()
        {
            OnCompleted = null;
            OnProgress = null;
        }
    }
}