using System;
using UnityEngine;

namespace Source.Root
{
    public class Bullet
    {
        private float _damage;
        private Vector3 _position;

        public Bullet(Vector3 position)
        {
            _position = position;
            StartPosition = position;
        }

        public float FlightSpeed { get; private set; } = 20f;
        public float MaxDistance { get; private set; } = 25f;
        public Vector3 StartPosition { get; private set; }
        public Vector3 CurrentPosition => _position;

        public event Action<Vector3> PositionChanged;

        public void ChangePosition(Vector3 position)
        {
            _position = position;
            PositionChanged?.Invoke(_position);
        }
    }
}
