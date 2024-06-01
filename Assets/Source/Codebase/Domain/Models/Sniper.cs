using System;
using UnityEngine;

namespace Source.Root
{
    public class Sniper : Character
    {
        private const float DefaultHitDistance = 10f;

        private readonly Camera _camera;

        public Sniper(float health) : base(health)
        {
            _camera = Camera.main;
        }

        public bool InAim { get; private set; }

        public event Action HeadShot;
        public event Action DoubleKill;
        public event Action HipShot;
        public event Action ThroughObstacleKilled;

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;

        public override Vector3 CalculateTrajectory(IGun gun, IBullet bullet)
        {
            int murdersCount = 0;
            int obstacleCount = 0;
            bool inAim = InAim;
            Ray ray = new(_camera.transform.position, _camera.transform.forward);
            RaycastHit[] results = Physics.RaycastAll(ray, gun.Range);

            if (results.Length == 0)
                return ray.origin + ray.direction * DefaultHitDistance;

            foreach (var result in results)
            {
                bullet.SetResult(result);

                if (result.transform.TryGetComponent(out IDamageable damageable))
                {
                    if (result.transform.TryGetComponent(out IBodyPart bodyPart))
                    {
                        if (inAim == false)
                            HipShot?.Invoke();

                        if (obstacleCount != 0)
                            ThroughObstacleKilled?.Invoke();

                        if (bodyPart.Name == BodyPartName.Head)
                            HeadShot?.Invoke();

                        murdersCount++;

                        if(murdersCount > 1)
                        {
                            DoubleKill?.Invoke();
                        }
                    }
                }
                else
                {
                    obstacleCount++;

                    if (obstacleCount > 1)
                        break;
                }
            }

            return gun.EndPoint.position + (results[0].point - gun.EndPoint.position);
        }
    }
}
