using System;
using UnityEngine;

namespace Source.Root
{
    public class GameLoopService
    {
        private BulletViewFactory _bulletViewFactory;

        public GameLoopService(BulletViewFactory bulletViewFactory)
        {
            _bulletViewFactory = bulletViewFactory;
        }

        public event Action<float> AimEnter;
        public event Action AimExit;
        public event Action Shot;

        public void EnterToAim(float animationLenht)
            => AimEnter?.Invoke(animationLenht);

        public void ExitOfAim()
            => AimExit?.Invoke();

        public void Shoot(Transform gunEnd)
        {
            BulletView bulletView = _bulletViewFactory.Create(gunEnd);
            Shot?.Invoke();
        }
    }
}
