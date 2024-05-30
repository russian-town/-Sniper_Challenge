using UnityEngine;

namespace Source.Root
{
    public interface IBullet
    {
        public void Fly(RaycastHit[] raycastHits);
    }
}
