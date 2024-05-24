using System;

namespace Source.Root
{
    public class AimingService
    {
        public event Action<float> AimEnter;
        public event Action AimExit;

        public void EnterToAim(float animationLenht)
            => AimEnter?.Invoke(animationLenht);

        public void ExitOfAim()
            => AimExit?.Invoke();
    }
}
