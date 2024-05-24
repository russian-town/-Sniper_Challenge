using System;

namespace Source.Root
{
    public class MobileInpute : IInput, IUpdatable
    {
        public event Action AimButtonDown;
        public event Action AimButtonUp;
        public event Action<float, float> AxisMoved;

        public void Update()
        {
        }
    }
}
