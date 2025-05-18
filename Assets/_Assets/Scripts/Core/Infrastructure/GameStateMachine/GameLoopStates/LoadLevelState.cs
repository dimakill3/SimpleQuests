using _Assets.Scripts.Core.Infrastructure.SceneManagement;
using _Assets.Scripts.Core.Infrastructure.StateMachine;
using _Assets.Scripts.Core.Infrastructure.WindowManagement;
using _Assets.Scripts.Core.UI;

namespace _Assets.Scripts.Core.Infrastructure.GameStateMachine.GameLoopStates
{
    public class LoadLevelState : GameLoopParamState<string>
    {
        private readonly LoadingScreen _loadingScreen;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(StateMachine.StateMachine stateMachine, WindowProvider windowProvider,
            ISceneLoader sceneLoader) : base(stateMachine)
        {
            _loadingScreen = windowProvider.GetWindow<LoadingScreen>();
            _sceneLoader = sceneLoader;
        }

        public override void OnEnter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded, true);
        }

        public override void OnExit()
        {
        }

        private void OnLoaded()
        {
            StateMachine.Enter<StartGameState>();
            _loadingScreen.Hide();
        }
    }
}