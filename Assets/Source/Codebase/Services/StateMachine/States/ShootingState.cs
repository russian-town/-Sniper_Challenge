using DG.Tweening;

namespace Source.Root
{
    public class ShootingState : State
    {
        private readonly CriminalView _view;

        private Sequence _sequence;

        public ShootingState(IStateMachine stateMachine, CriminalView view)
            : base(stateMachine)
        {
            _view = view;
        }

        public override void Enter()
        {
            _sequence = DOTween.Sequence();
            _view.Shoot();
            _sequence.AppendInterval(2f);
            _sequence.Play().Loops();
        }

        public override void Exit()
            => _sequence.Complete();
    }
}
