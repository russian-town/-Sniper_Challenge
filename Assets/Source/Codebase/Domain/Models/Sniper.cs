using System;
using UnityEngine;

namespace Source.Root
{
    public class Sniper : IDamageable
    {
        private readonly Health _health;

        public Sniper(float health)
        {
            _health = new(health);
            StartHealth = health;
        }

        public bool InAim { get; private set; }
        public float StartHealth { get; private set; }

        public event Action<float> HealthChanged;
        public event Action Died;

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;

        public void TakeDamage(float damage, Vector3 point)
        {
            _health.TakeDamage(damage, point);
            HealthChanged?.Invoke(_health.Value);

            if (_health.Value <= 0)
                Died?.Invoke();
        }
    }
}
