using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Codebase.Services.Abstract
{
    public abstract class BulletServiceBase
    {
        protected Transform GunEnd { get; private set; }
        protected float GunRange { get; private set; }
        protected Vector3 Position { get; private set; }

        public event Action<Vector3> PositionChanged;

        public abstract event Action FlightIsOver;

        public abstract void Fly(float bulletDamage);

        public void SetGunEnd(Transform gunEnd)
        {
            GunEnd = gunEnd;
            Position = gunEnd.position;
        }

        public void SetGunRange(float gunRange)
            => GunRange = gunRange;

        public async UniTask MoveTo(Vector3 target)
        {
            while (Vector3.Distance(Position, target) > 0)
            {
                Position =
                    Vector3.MoveTowards(Position, target, Time.deltaTime * 100f);
                PositionChanged?.Invoke(Position);
                await UniTask.Yield();
            }
        }
    }
}
