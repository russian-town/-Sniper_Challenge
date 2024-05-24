using UnityEngine;

namespace Source.Root
{
    public class CameraView : ViewBase
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _target;

        public Vector3 TargetPosition => _target.position;

        public void SetTarget(Transform target)
            => _target = target;

        public void SetRotation(float angle)
            => _transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }
}