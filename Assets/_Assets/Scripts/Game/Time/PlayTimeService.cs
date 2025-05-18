using System;
using _Assets.Scripts.Core.Infrastructure.EventManagement;
using _Assets.Scripts.Core.Infrastructure.Mono;
using _Assets.Scripts.Game.Events;
using Zenject;

namespace _Assets.Scripts.Game.Time
{
    public interface IPlayTimeService
    {
        event Action<int> OnSecondsChanged; 
        
        void StartPlay();
        void StopPlay();
    }

    public class PlayTimeService : IPlayTimeService
    {
        public event Action<int> OnSecondsChanged;
        
        private readonly MonoService _monoService;

        private float _playingTime = 0;

        [Inject]
        public PlayTimeService(MonoService monoService) =>
            _monoService = monoService;

        public void StartPlay()
        {
            _playingTime = 0;
            _monoService.OnTick += HandleTick;
        }

        public void StopPlay() =>
            _monoService.OnTick -= HandleTick;

        private void HandleTick()
        {
            var oldValue = (int) Math.Ceiling(_playingTime);
            _playingTime += UnityEngine.Time.deltaTime;

            var newValue = (int) Math.Ceiling(_playingTime);

            if (oldValue != newValue)
                OnSecondsChanged?.Invoke(newValue);
        }
    }
}