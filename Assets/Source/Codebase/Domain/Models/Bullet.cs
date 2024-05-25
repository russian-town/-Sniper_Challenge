using System;
using UnityEngine;

namespace Source.Root
{
    public class Bullet
    {
        private float _damage;
        private Vector3 _position;

        public event Action<Vector3> PositionChanged;

        public void ChangePosition(Vector3 position)
        {
            _position = position;
            PositionChanged?.Invoke(_position);
        }
    }
}
