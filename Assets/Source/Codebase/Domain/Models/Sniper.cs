using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            bool inAim = InAim;
            Ray ray = new(_camera.transform.position, _camera.transform.forward);
            RaycastHit[] results = Physics.RaycastAll(ray, gun.Range);

            if (results.Length == 0)
                return ray.origin + ray.direction * DefaultHitDistance;

            foreach (var result in results)
            {
                bullet.SetResult(result);
            }

            return gun.EndPoint.position + (results[0].point - gun.EndPoint.position);
        }
    }
}
