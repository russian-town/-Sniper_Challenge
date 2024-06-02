using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Source.Root
{
    public class ShootingState : State
    {
        private readonly CriminalView _view;

        private CancellationTokenSource _cancellationToken;

        public ShootingState(IStateMachine stateMachine, CriminalView view)
            : base(stateMachine)
        {
            _view = view;
        }

        public async override void Enter()
        {
            _cancellationToken = new();

            while (!_cancellationToken.IsCancellationRequested)
            {
                _view.Shoot();
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
            }
        }

        public override void Exit()
            => _cancellationToken.Cancel();
    }
}
