using UnityEngine;

namespace Source.Root
{
    public class Criminal : Character
    {
        private Transform _target;
        private Vector3 _position;

        public Criminal(float health) : base(health)
        {
        }

        public void SetTarget(Transform target)
            => _target = target;

        public void SetStartPosition(Vector3 position)
            => _position = position;

        public override Ray Ray()
            => new(_position, _target.position);
    }
}
