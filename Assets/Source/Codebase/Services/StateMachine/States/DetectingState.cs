using DG.Tweening;
using UnityEngine;

namespace Source.Root
{
    public class DetectingState : State
    {
        private readonly CriminalView _view;
        private Transform _sniper;
        
        private Sequence _sequence;

        public DetectingState(IStateMachine stateMachine, CriminalView view, Transform sniper)
            : base(stateMachine)
        {
            _view = view;
            _sniper = sniper;
        }

        public void SetSniper(Transform sniper)
            => _sniper = sniper;

        public override void Enter()
        {
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(2f);
            //_view.LookAtSniper(_sniper);
            StateMachine.Enter<ShootingState>();
        }

        public override void Exit()
            => _sequence.Complete();
    }
}
