using System;
using UnityEngine;

namespace Source.Root
{
    public class Gun
    {
        private readonly Camera _camera;

        private GunConfig _config;
        private Transform _gunEnd;

        public event Action<Vector3> Shot;

        public Gun()
            => _camera = Camera.main;

        public void SetGunEnd(Transform gunEnd)
            => _gunEnd = gunEnd;

        public void SetConfig(GunConfig config)
            => _config = config;

        public void Shoot()
            => Shot?.Invoke(CalculatePosition());

        private Vector3 CalculatePosition()
        {
            Ray ray = new(_camera.transform.position, _camera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.transform.TryGetComponent(out CriminalView criminal))
                    Debug.Log("Poop");

                return hitInfo.point;
            }

            return _gunEnd.forward * _config.Range + _gunEnd.position;
        }
    }
}
