using UnityEngine;

namespace Source.Root
{
    public class CameraView : ViewBase
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _target;

        [field: SerializeField] public Vector3 OffSet { get; private set; }
        public Vector3 TargetPosition => _target.position;


        public void SetRotation(float angle)
            => _transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }
}