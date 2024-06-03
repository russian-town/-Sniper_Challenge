using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Root
{
    public class Bullet : IBullet
    {
        private readonly BulletConfig _config;
        private readonly Stack<RaycastHit> _results;

        private Vector3 _position;

        public Bullet(Vector3 position, BulletConfig config)
        {
            _position = position;
            _config = config;
            _results = new();
            StartPosition = position;
        }

        public Vector3 StartPosition { get; private set; }
        public Vector3 CurrentPosition => _position;
        public float FlightSpeed => _config.FlightSpeed;
        public float Damage => _config.Damage;

        public event Action<Vector3> PositionChanged;
        public event Action<Vector3> FlewOut;

        public void SetResult(RaycastHit result)
            => _results.Push(result);

        public void ChangePosition(Vector3 position)
        {
            _position = position;
            PositionChanged?.Invoke(_position);
        }

        public bool GoalAchieved(Vector3 target)
        {
            float distance = (target - _position).sqrMagnitude;
            return Mathf.Approximately(Mathf.Sqrt(distance), 0f);
        }

        public void UpdateResults()
        {
            if (_results.Count == 0)
                return;

            float distance = (_results.Peek().point - _position).sqrMagnitude;

            if (Mathf.Sqrt(distance) < 0.5f)
            {
                RaycastHit raycastHit = _results.Pop();

                if (raycastHit.transform.TryGetComponent(out IDamageable damageable))
                    damageable.TakeDamage(_config.Damage, raycastHit.point);
            }
        }

        public void StartFlight(Vector3 point)
            => FlewOut?.Invoke(point);
    }
}
