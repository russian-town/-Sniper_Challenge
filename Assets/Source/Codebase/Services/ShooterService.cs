using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;

namespace Source.Root
{
    public class ShooterService
    {
        private readonly BulletView _bulletViewTemplate;
        private readonly BulletConfig _bulletConfig;
        private readonly Dictionary<Character, IGun> _findGunOfCharacter;

        public ShooterService(
            BulletView bulletViewTemplate,
            BulletConfig bulletConfig)
        {
            _bulletViewTemplate = bulletViewTemplate;
            _bulletConfig = bulletConfig;
            _findGunOfCharacter = new();
        }

        public void RegistyWeapon(Character character, IGun gun)
            => _findGunOfCharacter.Add(character, gun);

        public void UnregistryWeapon(Character character)
            => _findGunOfCharacter.Remove(character);

        public void CreateBullet(Character character)
        {
            IGun gun = _findGunOfCharacter[character] ?? throw new Exception("Weapon unregistry");
            Bullet bullet = new(gun.EndPoint.position, _bulletConfig);
            BulletView bulletView =
                Object.Instantiate(_bulletViewTemplate, gun.EndPoint.position, Quaternion.identity);
            BulletPresenter bulletPresenter = new(bullet, bulletView);
            bulletView.Construct(bulletPresenter);
            Vector3 trajectory = character.CalculateTrajectory(gun, bullet);
            bullet.StartFlight(trajectory);
        }
    }
}
