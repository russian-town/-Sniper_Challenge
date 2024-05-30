using System;
using UnityEngine;

namespace Source.Root
{
    public class Bullet : IBullet
    {
        private readonly BulletConfig _config;

        private Vector3 _position;

        public Bullet(Vector3 position, BulletConfig config)
        {
            _position = position;
            _config = config;
            StartPosition = position;
        }

        public Vector3 StartPosition { get; private set; }
        public Vector3 CurrentPosition => _position;
        public float FlightSpeed => _config.FlightSpeed;

        public event Action<Vector3> PositionChanged;
        public event Action<RaycastHit[]> FlewOut;

        public void Attack(RaycastHit hit)
        {
            if (hit.transform.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_config.Damage, hit.point);
        }

        public void ChangePosition(Vector3 position)
        {
            _position = position;
            PositionChanged?.Invoke(_position);
        }

        public void Fly(RaycastHit[] raycastHits)
        {
            FlewOut?.Invoke(raycastHits);
        }
    }
}
