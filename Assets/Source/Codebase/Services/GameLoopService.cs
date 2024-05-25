using System;
using UnityEngine;

namespace Source.Root
{
    public class GameLoopService
    {
        private readonly BulletViewFactory _bulletViewFactory;

        public GameLoopService(BulletViewFactory bulletViewFactory)
        {
            _bulletViewFactory = bulletViewFactory;
        }

        public event Action<float> AimEnter;
        public event Action<float> AimExit;
        public event Action Shot;
        public event Action<float> CameraRotationChanged;

        public void EnterToAim(float animationLenht)
            => AimEnter?.Invoke(animationLenht);

        public void ExitOfAim(float animationLenht)
            => AimExit?.Invoke(animationLenht);

        public void Shoot(Transform gunEnd)
        {
            _bulletViewFactory.Create(gunEnd);
            Shot?.Invoke();
        }

        public void CallCameraEvent(float angle)
            => CameraRotationChanged?.Invoke(angle);
    }
}
