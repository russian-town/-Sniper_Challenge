using System;
using UnityEngine;

namespace Source.Root
{
    public class Criminal : ICharacter, IDamageable
    {
        private readonly Health _health;

        private Transform _target;

        public event Action<float> HealthChanged;
        public event Action<float, Vector3> DamageProcessed;
        public event Action Died;

        public float StartHealth { get; private set; }

        public Criminal(float health)
        {
            _health = new(health);
            StartHealth = health;
        }

        public void SetTarget(Transform target)
            => _target = target;

        public Vector3 CalculateTrajectory(IGun gun, IBullet bullet)
        {
            Vector3 direction = _target.position - gun.EndPoint.position;
            Ray ray = new(gun.EndPoint.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, gun.Range))
            {
                bullet.SetResult(hitInfo);
                return gun.EndPoint.position + (hitInfo.point - gun.EndPoint.position);
            }
            else
            {
                return gun.EndPoint.position + gun.EndPoint.forward * gun.Range;
            }
        }

        public void TakeDamage(float damage, Vector3 point)
        {
            _health.TakeDamage(damage, point);

            if(_health.Value <= 0)
            {
                Died?.Invoke();
                return;
            }

            HealthChanged?.Invoke(_health.Value);
            DamageProcessed?.Invoke(damage, point);
        }
    }
}
