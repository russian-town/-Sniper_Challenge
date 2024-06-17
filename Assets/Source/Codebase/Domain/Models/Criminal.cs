using System;
using UnityEngine;

namespace Source.Root
{
    public class Criminal : IDamageable
    {
        private readonly Health _health;

        public event Action<float> HealthChanged;
        public event Action<float, Vector3> DamageProcessed;
        public event Action Died;

        public float StartHealth { get; private set; }

        public Criminal(float health)
        {
            _health = new(health);
            StartHealth = health;
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
