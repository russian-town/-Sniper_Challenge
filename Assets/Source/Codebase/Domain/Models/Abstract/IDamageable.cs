using UnityEngine;

namespace Source.Root
{
    public interface IDamageable
    {
        public void ProcessCalculatedDamage(float damage, Vector3 point);
    }
}