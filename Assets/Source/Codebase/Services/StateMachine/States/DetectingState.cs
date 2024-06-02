using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Root
{
    public class DetectingState : State
    {
        private readonly CriminalView _view;

        public DetectingState(IStateMachine stateMachine, CriminalView view, Transform sniper)
            : base(stateMachine)
        {
            _view = view;
        }

        public override async void Enter()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            _view.LookAtSniper();
            StateMachine.Enter<ShootingState>();
        }

        public override void Exit() { }
    }
}
