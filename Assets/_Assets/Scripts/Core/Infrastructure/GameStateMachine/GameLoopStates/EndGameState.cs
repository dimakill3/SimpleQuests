using _Assets.Scripts.Core.Infrastructure.Configs;
using _Assets.Scripts.Core.Infrastructure.StateMachine;

namespace _Assets.Scripts.Core.Infrastructure.GameStateMachine.GameLoopStates
{
    public class EndGameState : GameLoopState
    {
        private readonly GameConfig _gameConfig;

        public EndGameState(StateMachine.StateMachine stateMachine, GameConfig gameConfig) : base(stateMachine)
        {
            _gameConfig = gameConfig;
        }

        public override void OnEnter()
        {
            StateMachine.Enter<LoadLevelState, string>(_gameConfig.StartLevelScene);
        }
    }
}