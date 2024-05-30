using System;
using UnityEngine;

namespace Source.Root
{
    public class Gun : IGun
    {
        private RaycastHit[] _raycastHits;
        private GunConfig _config;
        private Transform _endPoint;

        public Transform EndPoint => _endPoint;

        public event Action Shot;

        public void SetGunEnd(Transform gunEnd)
            => _endPoint = gunEnd;

        public void SetConfig(GunConfig config)
            => _config = config;

        public void ApplyTrajectori(Ray ray)
        {
            _raycastHits = Physics.RaycastAll(ray, _config.Range);
        }

        public void TakeBullet(IBullet bullet)
        {
            bullet.Fly(_raycastHits);
            Shot?.Invoke();
        }
    }
}
