using System.Linq;
using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Core.Infrastructure.StateMachine;
using _Assets.Scripts.Game.Enemies.Spawner;
using _Assets.Scripts.Game.Map;
using _Assets.Scripts.Game.Quests.Services;
using _Assets.Scripts.Game.Time;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Assets.Scripts.Core.Infrastructure.GameStateMachine.GameLoopStates
{
    public class StartGameState : GameLoopState
    {
        private readonly GameConfig _gameConfig;
        private IEnemySpawner _spawner;
        private IMapConfigurator _mapConfigurator;
        private IQuestsProgression _questsProgression;
        private IPlayTimeService _playTimeService;

        public StartGameState(StateMachine.StateMachine stateMachine, GameConfig gameConfig) : base(stateMachine)
        {
            _gameConfig = gameConfig;
        }

        public override void OnEnter()
        {
            UpdateSceneContext();
            
            _mapConfigurator.ConfigureMap(_gameConfig.MapConfig.MapSize);
            _questsProgression.StartProgression();
            _spawner.StartSpawn();
            
            _playTimeService.StartPlay();

            _questsProgression.AllQuestsCompleted += EndGame;
        }

        public override void OnExit()
        {
            _playTimeService.StopPlay();
            _spawner.StopSpawn();
            
            _questsProgression.Dispose();
            _spawner.Dispose();
        }

        private void UpdateSceneContext()
        {
            var sceneContext = SceneManager.GetActiveScene().GetRootGameObjects().First(x => x.GetComponent<SceneContext>()).GetComponent<SceneContext>();
            if (sceneContext == null)
                return;
            
            _spawner = sceneContext.Container.Resolve<IEnemySpawner>();
            _mapConfigurator = sceneContext.Container.Resolve<IMapConfigurator>();
            _questsProgression = sceneContext.Container.Resolve<IQuestsProgression>();
            _questsProgression = sceneContext.Container.Resolve<IQuestsProgression>();
            _playTimeService = sceneContext.Container.Resolve<IPlayTimeService>();
        }

        private void EndGame()
        {
            _questsProgression.AllQuestsCompleted -= EndGame;
            StateMachine.Enter<EndGameState>();
        }
    }
}