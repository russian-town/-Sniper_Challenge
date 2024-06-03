using UnityEngine;

namespace Source.Root
{
    public interface IDamageable
    {
        public void TakeDamage(float damage, Vector3 point);
    }
}