using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Codebase.Services;

namespace Source.Root
{
    public class ShootingState : State
    {
        private readonly IKService _ikService;
        private readonly CriminalView _view;
        private readonly CancellationTokenSource _token;

        public ShootingState(
            IStateMachine stateMachine,
            IKService ikService,
            CriminalView view)
            : base(stateMachine)
        {
            _ikService = ikService;
            _view = view;
            _token = new();
        }

        public async override void Enter()
        {
            _ikService.Initialize();

            while (!_token.IsCancellationRequested)
            {
                _view.Shoot();
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
            }
        }

        public override void Update()
            => _ikService.UpdateBones(1f, 180f, 3f);

        public override void Exit()
            => _token.Cancel();
    }
}
