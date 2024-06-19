using System;
using Source.Codebase.Services.Abstract;
using Source.Root;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class CriminalBulletService : BulletServiceBase
    {
        private readonly Transform _target;

        public CriminalBulletService(Transform target)
        {
            _target = target;
        }

        public override event Action FlightIsOver;

        public override async void Fly(float bulletDamage)
        {
            Vector3 direction = _target.position - GunEnd.position;
            Ray ray = new(GunEnd.position, direction);
            Vector3 targetPosition;

            if (Physics.Raycast(ray, out RaycastHit hitInfo, GunRange))
            {
                targetPosition = GunEnd.position + (hitInfo.point - GunEnd.position);
                await MoveTo(targetPosition);

                if (hitInfo.transform.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(bulletDamage, hitInfo.point);
            }
            else
            {
                targetPosition = GunEnd.position + GunEnd.forward * GunRange;
                await MoveTo(targetPosition);
            }

            FlightIsOver?.Invoke();
        }
    }
}
