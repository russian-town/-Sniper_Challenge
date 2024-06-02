using System;
using Cysharp.Threading.Tasks;

namespace Source.Root
{
    public class LookingState : State
    {
        private readonly CriminalView _view;

        public LookingState(IStateMachine stateMachine, CriminalView view) :
            base(stateMachine)
        {
            _view = view;
        }

        public async override void Enter()
        {
            _view.FindSniper();
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            StateMachine.Enter<DetectingState>();
        }

        public override void Exit() { }
    }
}
