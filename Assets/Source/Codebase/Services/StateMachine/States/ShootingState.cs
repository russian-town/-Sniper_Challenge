using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Codebase.Services;

namespace Source.Root
{
    public class ShootingState : State
    {
        private readonly CriminalView _view;
        private readonly CancellationTokenSource _token;

        public ShootingState(
            IStateMachine stateMachine,
            CriminalView view)
            : base(stateMachine)
        {
            _view = view;
            _token = new();
        }

        public async override void Enter()
        {
            while (!_token.IsCancellationRequested)
            {
                _view.Shoot();
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
            }
        }

        public override void Update() { }

        public override void Exit()
            => _token.Cancel();
    }
}
