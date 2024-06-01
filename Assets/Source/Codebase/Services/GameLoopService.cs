using System;
using UnityEngine;

namespace Source.Root
{
    public class GameLoopService
    {
        public event Action<float> AimEnter;
        public event Action<float> AimExit;
        public event Action Shot;
        public event Action<float> CameraRotationChanged;
        public event Action<Transform> SniperShot;
        public event Action SniperDied;

        public void EnterToAim(float animationLenht)
            => AimEnter?.Invoke(animationLenht);

        public void ExitOfAim(float animationLenht)
            => AimExit?.Invoke(animationLenht);

        public void SniperShoot(Transform point)
        {
            Shot?.Invoke();
            SniperShot?.Invoke(point);
        }

        public void CallCameraEvent(float angle)
            => CameraRotationChanged?.Invoke(angle);
    }
}
