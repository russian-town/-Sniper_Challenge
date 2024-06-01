using DG.Tweening;

namespace Source.Root
{
    public class LookingState : State
    {
        private readonly CriminalView _view;

        private Sequence _sequence;

        public LookingState(IStateMachine stateMachine, CriminalView view) :
            base(stateMachine)
        {
            _view = view;
        }

        public override void Enter()
        {
            _view.FindSniper();
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(1f);
            _sequence.Complete();
            StateMachine.Enter<DetectingState>();
        }

        public override void Exit()
            => _sequence.Complete();
    }
}
