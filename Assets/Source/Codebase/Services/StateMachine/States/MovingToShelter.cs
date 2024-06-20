using Source.Root;

namespace Source.Codebase.Services
{
    public class MovingToShelter : State
    {
        public MovingToShelter(IStateMachine stateMachine)
            : base(stateMachine) { }

        public override void Enter() { }

        public override void Exit() { }
    }
}
