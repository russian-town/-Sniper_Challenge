using System;
using UnityEngine;

namespace Source.Root
{
    public class Gun
    {
        private readonly Camera _camera;

        private GunConfig _config;
        private Transform _gunEnd;

        public event Action<RaycastHit[]> Shot;

        public Gun()
            => _camera = Camera.main;

        public void SetGunEnd(Transform gunEnd)
            => _gunEnd = gunEnd;

        public void SetConfig(GunConfig config)
            => _config = config;

        public void Shoot()
        {
            Ray ray = new(_camera.transform.position, _camera.transform.forward);
            RaycastHit[] raycastHit = Physics.RaycastAll(ray, _config.Range);
            Shot?.Invoke(raycastHit);
            Vector3 direction = ray.direction * _config.Range + _gunEnd.position;
        }
    }
}
