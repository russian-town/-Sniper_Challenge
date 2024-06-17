using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Codebase.Services.Abstract;
using Source.Root;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class SniperBulletService : BulletServiceBase
    {
        private readonly AchievementFactory _achievementFactory;
        private readonly Camera _camera;
        private readonly bool InAim;

        public SniperBulletService(
            AchievementFactory achievementFactory,
            bool inAim)
        {
            _achievementFactory = achievementFactory;
            _camera = Camera.main;
            InAim = inAim;
        }

        public override event Action FlightIsOver;

        public override async void Fly(float bulletDamage)
        {
            Vector3 targetPosition = Vector3.zero;
            Ray ray = new(_camera.transform.position, _camera.transform.forward);
            RaycastHit[] results = Physics.RaycastAll(ray, GunRange);

            if (results.Length == 0)
            {
                targetPosition = ray.origin + ray.direction * GunRange;
                await MoveTo(targetPosition);
                FlightIsOver?.Invoke();
                return;
            }

            HashSet<CriminalView> criminals = new();
            CriminalView currentCriminal = null;

            for (int i = 0; i < results.Length; i++)
            {
                await MoveTo(results[i].point);

                if (results[i].transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(bulletDamage, results[i].point);

                    if (results[i].transform.TryGetComponent(out IBodyPart bodyPart))
                    {
                        if (bodyPart.CriminalView == currentCriminal)
                            continue;

                        currentCriminal = bodyPart.CriminalView;

                        if (bodyPart.CheckDead(bulletDamage))
                        {
                            criminals.Add(bodyPart.CriminalView);

                            if (bodyPart.Name == BodyPartName.Head)
                                _achievementFactory.Create(AchievementsType.HeadShot);

                            if (InAim == false)
                                _achievementFactory.Create(AchievementsType.HipfireShot);

                            if (i == 0)
                                continue;

                            RaycastHit previousResult = results[i - 1];

                            if (!previousResult.transform.TryGetComponent(out IDamageable _))
                                if (results[i].distance > previousResult.distance)
                                    _achievementFactory.Create(AchievementsType.ThroughCoverHit);
                        }
                    }

                    if (criminals.Count > 1)
                        _achievementFactory.Create(AchievementsType.MultiKill);
                }
            }

            FlightIsOver?.Invoke();
        }
    }
}
