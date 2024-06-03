using System;
using UnityEngine;

namespace Source.Root
{
    public interface ICharacter
    {
        public float StartHealth { get; }

        public event Action<float> HealthChanged;

        public abstract Vector3 CalculateTrajectory(IGun gun, IBullet bullet);
    }
}
