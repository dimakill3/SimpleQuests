using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Core.Infrastructure.StateMachine;
using _Assets.Scripts.Core.Infrastructure.WindowManagement;
using _Assets.Scripts.Game.Enemies.Spawner;
using _Assets.Scripts.Game.Map;
using Zenject;

namespace _Assets.Scripts.Core.Infrastructure.GameStateMachine.GameLoopStates
{
    public class StartGameState : GameLoopState
    {
        private readonly WindowProvider _windowProvider;
        private readonly GameConfig _gameConfig;
        private readonly DiContainer _diContainer;
        private IEnemySpawner _spawner;
        private IMapConfigurator _mapConfigurator;

        public StartGameState(StateMachine.StateMachine stateMachine, WindowProvider windowProvider,
            GameConfig gameConfig, DiContainer diContainer) : base(stateMachine)
        {
            _windowProvider = windowProvider;
            _gameConfig = gameConfig;
            _diContainer = diContainer;
        }

        public override void OnEnter()
        {
            _spawner = _diContainer.Resolve<IEnemySpawner>();
            _mapConfigurator = _diContainer.Resolve<IMapConfigurator>();
            
            _mapConfigurator.ConfigureMap(_gameConfig.MapConfig.MapSize);
            _spawner.StartSpawn();
        }

        public override void OnExit()
        {
            _spawner.StopSpawn();
        }
    }
}