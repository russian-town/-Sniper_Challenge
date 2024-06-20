using System;
using Cysharp.Threading.Tasks;

namespace Source.Root
{
    public class DetectingState : State
    {
        public DetectingState(IStateMachine stateMachine)
            : base(stateMachine) { }

        public override async void Enter()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            StateMachine.Enter<ShootingState>();
        }

        public override void Exit() { }
    }
}
