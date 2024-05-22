using System;

namespace Source.Root
{
    public class AimingService
    {
        private bool _isAim = false;

        public event Action AimEnter;
        public event Action AimExit;

        public void Aim()
        {
            if (_isAim == false)
                AimEnter?.Invoke();
            else
                AimExit?.Invoke();
        }
    }
}
