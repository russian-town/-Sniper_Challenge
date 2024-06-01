namespace Source.Root
{
    public class IdleState : State
    {
        public IdleState(IStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
            => StateMachine.SniperDetected += OnSniperDetected;

        public override void Exit()
            => StateMachine.SniperDetected -= OnSniperDetected;

        private void OnSniperDetected()
            => StateMachine.Enter<LookingState>();
    }
}
