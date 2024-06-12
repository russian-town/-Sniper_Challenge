using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Root
{
    public class Sniper : ICharacter, IDamageable
    {
        private const float DefaultHitDistance = 10f;

        private readonly Camera _camera;
        private readonly Health _health;

        public Sniper(float health)
        {
            _camera = Camera.main;
            _health = new(health);
            StartHealth = health;
        }

        public bool InAim { get; private set; }
        public float StartHealth { get; private set; }

        public event Action<float> HealthChanged;
        public event Action HeadShot;
        public event Action HipfireShot;
        public event Action<int> MultiKill;
        public event Action ThroughCoverHit;
        public event Action Died;

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;

        public void TakeDamage(float damage, Vector3 point)
        {
            _health.TakeDamage(damage, point);
            HealthChanged?.Invoke(_health.Value);

            if (_health.Value <= 0)
                Died?.Invoke();
        }

        public Vector3 CalculateTrajectory(IGun gun, IBullet bullet)
        {
            bool inAim = InAim;
            Ray ray = new(_camera.transform.position, _camera.transform.forward);
            RaycastHit[] results = Physics.RaycastAll(ray, gun.Range);

            if (results.Length == 0)
                return ray.origin + ray.direction * DefaultHitDistance;

            HashSet<CriminalView> criminals = new();
            CriminalView currentCriminal = null;

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].transform.TryGetComponent(out IBodyPart bodyPart))
                {
                    if (bodyPart.CriminalView == currentCriminal)
                        continue;

                    currentCriminal = bodyPart.CriminalView;

                    if (bodyPart.CheckDead(bullet.Damage))
                    {
                        criminals.Add(bodyPart.CriminalView);

                        if (bodyPart.Name == BodyPartName.Head)
                            HeadShot?.Invoke();

                        if (inAim == false)
                            HipfireShot?.Invoke();

                        if (i == 0)
                            continue;

                        RaycastHit previousResult = results[i - 1];

                        if (!previousResult.transform.TryGetComponent(out IDamageable _))
                            if (results[i].distance > previousResult.distance)
                                ThroughCoverHit?.Invoke();
                    }
                }

                if (criminals.Count > 1)
                    MultiKill?.Invoke(criminals.Count);
            }

            foreach (var result in results)
            {
                bullet.SetResult(result);
            }

            return gun.EndPoint.position + (results[0].point - gun.EndPoint.position);
        }
    }
}
