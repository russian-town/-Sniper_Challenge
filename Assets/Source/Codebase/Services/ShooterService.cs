using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;

namespace Source.Root
{
    public class ShooterService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly BulletView _bulletViewTemplate;
        private readonly BulletConfig _bulletConfig;
        private readonly Dictionary<Character, IGun> _findGunOfCharacter;

        public ShooterService(
            ICoroutineRunner coroutineRunner,
            BulletView bulletViewTemplate,
            BulletConfig bulletConfig)
        {
            _coroutineRunner = coroutineRunner;
            _bulletViewTemplate = bulletViewTemplate;
            _bulletConfig = bulletConfig;
            _findGunOfCharacter = new();
        }

        public void RegistyWeapon(Character character, IGun gun)
            => _findGunOfCharacter.Add(character, gun);

        public void CreateBullet(Character character)
        {
            IGun gun = _findGunOfCharacter[character] ?? throw new Exception("Weapon unregistry");
            Bullet bullet = new(gun.EndPoint.position, _bulletConfig);
            BulletView bulletView =
                Object.Instantiate(_bulletViewTemplate, gun.EndPoint.position, Quaternion.identity);
            BulletPresenter bulletPresenter = new(bullet, bulletView, _coroutineRunner);
            bulletView.Construct(bulletPresenter);
            Vector3 trajectory = character.CalculateTrajectory(gun, bullet);
            bullet.StartFlight(trajectory);
        }
    }
}
