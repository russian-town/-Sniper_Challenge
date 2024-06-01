using System;

namespace Source.Root
{
    public interface IStateMachine
    {
        public event Action SniperDetected;

        public void Enter<T>() where T : State;
    }
}
