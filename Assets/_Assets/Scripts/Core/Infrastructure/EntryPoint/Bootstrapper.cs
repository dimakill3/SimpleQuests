using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Core.Infrastructure.GameStateMachine.GameLoopStates;
using _Assets.Scripts.Core.Infrastructure.SceneManagement;
using _Assets.Scripts.Core.Infrastructure.WindowManagement;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace _Assets.Scripts.Core.Infrastructure.EntryPoint
{
    public class Bootstrapper : IInitializable
    {
        private StateMachine.StateMachine _stateMachine;
        
        private readonly ISceneLoader _sceneLoader;
        private readonly WindowProvider _windowProvider;
        private readonly GameConfig _gameConfig;
        private readonly DiContainer _container;

        [Inject]
        public Bootstrapper(ISceneLoader sceneLoader, WindowProvider windowProvider,GameConfig gameConfig, DiContainer container)
        {
            _sceneLoader = sceneLoader;
            _windowProvider = windowProvider;
            _gameConfig = gameConfig;
            _container = container;
        }

        public void Initialize()
        {
            InitializeStateMachine();
            StartGame();
        }

        private void InitializeStateMachine()
        {
            _stateMachine = new StateMachine.StateMachine();
            _stateMachine.AddState(new LoadLevelState(_stateMachine, _windowProvider, _sceneLoader));
            _stateMachine.AddState(new StartGameState(_stateMachine, _gameConfig));
            _stateMachine.AddState(new EndGameState(_stateMachine, _gameConfig));
        }

        private void StartGame() =>
            _stateMachine.Enter<LoadLevelState, string>(_gameConfig.StartLevelScene);
    }
}