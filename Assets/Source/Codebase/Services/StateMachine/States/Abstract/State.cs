namespace Source.Root
{
    public abstract class State
    {
        public State(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }

        protected IStateMachine StateMachine { get; private set; }

        public abstract void Enter();
        public abstract void Exit();
    }
}
