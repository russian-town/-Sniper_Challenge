using UnityEngine;

namespace Source.Root
{
    public class BulletView : ViewBase
    {
        [SerializeField] private Transform _transform;

        public void SetPosition(Vector3 position)
            => _transform.position = position;

        public void SetDirection(Vector3 direction)
            => _transform.rotation = Quaternion.LookRotation(direction);
    }
}
