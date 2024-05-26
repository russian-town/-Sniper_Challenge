using System;

namespace Source.Root
{
    public interface IInput
    {
        public event Action AimButtonDown;
        public event Action ShootButtonDown;
        public event Action<float, float> AxisMoved;
    }
}
