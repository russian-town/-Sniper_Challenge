using System;
using UnityEngine;

namespace Source.Root
{
    public class Gun : IGun
    {
        private GunConfig _config;
        private Transform _endPoint;

        public Transform EndPoint => _endPoint;
        public float Range => _config.Range;

        public event Action Shot;

        public void SetEndPoint(Transform endPoint)
            => _endPoint = endPoint;

        public void SetConfig(GunConfig config)
            => _config = config;

        public void Shoot()
            => Shot?.Invoke();
    }
}
