using System;
using UnityEngine;

namespace Source.Root
{
    public class Health : IDamageable
    {
        public Health(float value)
            => Value = value;

        public float Value { get; private set; }

        public void TakeDamage(float damage, Vector3 point)
        {
            if (damage <= 0)
                throw new ArgumentException("Damage cannot be negative or zero");

            if (Value <= 0)
                Value = 0;
        }
    }
}
