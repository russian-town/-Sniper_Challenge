using UnityEngine;

namespace Source.Root
{
    public class BulletView : ViewBase
    {
        [SerializeField] private Transform _transform;

        public void SetPosition(Vector3 position)
            => _transform.position = position;

        public void SetRotation(Quaternion rotation)
            => _transform.rotation = rotation;
    }
}
