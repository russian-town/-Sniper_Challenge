using System;

namespace Source.Root
{
    public abstract class Character : IDamageable
    {
        private float _health;

        public event Action Died;

        public Character(float health)
            => _health = health;

        public void TakeDamage(float damage)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage cannot be negative or zero");

            if (_health <= 0)
            {
                _health = 0;
                Died?.Invoke();
            }
        }
    }
}
