using System;
using UnityEngine;

namespace Source.Root
{
    public class Sniper : Character, IDamageable
    {
        private const float DefaultHitDistance = 10f;

        private readonly Camera _camera;

        public Sniper(float health) : base(health)
        {
            _camera = Camera.main;
        }

        public bool InAim { get; private set; }

        public event Action<RaycastHit[]> TargetsHit;

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;

        public override Vector3 CalculateTrajectory(IGun gun, IBullet bullet)
        {
            Ray ray = new(_camera.transform.position, _camera.transform.forward);
            RaycastHit[] results = Physics.RaycastAll(ray, gun.Range);

            if (results.Length == 0)
                return ray.origin + ray.direction * DefaultHitDistance;

            foreach (var result in results)
            {
                bullet.Attack(result);
            }

            return gun.EndPoint.position + (results[^1].point - gun.EndPoint.position);
        }
    }
}
