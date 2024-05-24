using System;
using UnityEngine;

namespace Source.Root
{
    public class Bullet
    {
        private BulletConfig _config;
        private float _damage;
        private Vector3 _position;

        public Bullet(BulletConfig config)
        {
            _config = config;
            _damage = _config.Damage;
        }

        public event Action<Vector3> PositionChanged;

        public void ChangePosition(Vector3 position)
        {
            _position = position;
            PositionChanged?.Invoke(_position);
        }

    }
}
