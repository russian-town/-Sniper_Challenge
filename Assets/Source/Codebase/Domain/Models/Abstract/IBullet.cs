using UnityEngine;

namespace Source.Root
{
    public interface IBullet
    {
        public void Attack(RaycastHit result);
    }
}
