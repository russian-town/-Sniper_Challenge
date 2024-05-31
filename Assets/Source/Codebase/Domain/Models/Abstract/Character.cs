using System;
using UnityEngine;

namespace Source.Root
{
    public abstract class Character
    {
        private readonly Health _health;

        public Character(float health)
            => _health = new(health);

        public event Action<Vector3> Died;
        public event Action<float, Vector3> DamageProcessed;

        public void TakeDamage(float damage, Vector3 point)
        {
            _health.TakeDamage(damage, point);

            if (_health.Value <= 0)
            {
                Died?.Invoke(point);
                return;
            }

            DamageProcessed?.Invoke(damage, point);
        }

        public abstract Vector3 CalculateTrajectory(IGun gun, IBullet bullet);
    }
}
