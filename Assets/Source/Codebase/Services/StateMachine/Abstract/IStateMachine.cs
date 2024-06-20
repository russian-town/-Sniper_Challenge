using System;

namespace Source.Root
{
    public interface IStateMachine
    {
        public void Enter<T>() where T : State;
    }
}
