using System;
using UnityEngine;

namespace Source.Root
{
    public class Bullet
    {
        private Vector3 _position;

        public Bullet(Vector3 position, Vector3 direction)
        {
            _position = position;
            StartPosition = position;
            Direction = direction;
        }

        public Vector3 Direction { get; private set; }
        public float FlightSpeed { get; private set; } = 20f;
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
