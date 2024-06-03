using UnityEngine;

namespace Source.Root
{
    public interface IBullet
    {
        public float Damage { get; }

        public void SetResult(RaycastHit result);
    }
}
