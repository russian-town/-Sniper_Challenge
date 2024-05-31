using UnityEngine;

namespace Source.Root
{
    public class BulletView : ViewBase
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private float _flightDuration = .35f;

        public void SetDirection(Vector3 direction)
            => _transform.LookAt(direction);

        public void SetPosition(Vector3 position)
            => _transform.position = position;
    }
}
