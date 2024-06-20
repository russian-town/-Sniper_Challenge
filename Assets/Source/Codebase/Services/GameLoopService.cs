using System;
using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Root
{
    public class GameLoopService
    {
        public event Action AimEnter;
        public event Action AimExit;
        public event Action<GunType> Shot;
        public event Action SniperShot;
        public event Action<Transform> SniperCreated;
        public event Action SniperDied;

        public void EnterToAim()
            => AimEnter?.Invoke();

        public void ExitOfAim()
            => AimExit?.Invoke();

        public void SniperShoot(GunType gunType)
        {
            Shot?.Invoke(gunType);
            SniperShot?.Invoke();
        }

        public void CallEventOfSniperCreated(Transform sniper)
            => SniperCreated?.Invoke(sniper);

        public void CallEventOfSniperDied()
            => SniperDied?.Invoke();
    }
}
