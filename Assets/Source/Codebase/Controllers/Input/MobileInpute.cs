using System;

namespace Source.Root
{
    public class MobileInpute : IInput, IUpdatable
    {
        public event Action AimButtonDown;
        public event Action AimButtonUp;

        public void Update()
        {
        }
    }
}
