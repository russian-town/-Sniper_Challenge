using System;
using UnityEngine;

namespace Source.Root
{
    public class Criminal : Character, IDamageable
    {
        private readonly Health _health;

        public Criminal(float health)
            => _health = new(health);

        public event Action<Vector3> Died;
        public event Action<float, Vector3> DamageRecived;

        public void TakeDamage(float damage, Vector3 point)
        {
            _health.TakeDamage(damage, point);

            if (_health.Value <= 0)
            {
                Died?.Invoke(point);
                return;
            }

            DamageRecived?.Invoke(damage, point);
        }
    }
}
