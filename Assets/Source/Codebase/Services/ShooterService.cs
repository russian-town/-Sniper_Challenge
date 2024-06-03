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
        private readonly Dictionary<ICharacter, IGun> _findGunOfCharacter;

        public ShooterService(
            BulletView bulletViewTemplate,
            BulletConfig bulletConfig)
        {
            _bulletViewTemplate = bulletViewTemplate;
            _bulletConfig = bulletConfig;
            _findGunOfCharacter = new();
        }

        public event Action<IGun, IBullet> BulletCreated;

        public void RegistyWeapon(ICharacter character, IGun gun)
            => _findGunOfCharacter.Add(character, gun);

        public void UnregistryWeapon(ICharacter character)
            => _findGunOfCharacter.Remove(character);

        public void CreateBullet(ICharacter character)
        {
            IGun gun = _findGunOfCharacter[character] ?? throw new Exception("Weapon unregistry!");
            gun.Shoot();
            Bullet bullet = new(gun.EndPoint.position, _bulletConfig);
            BulletView bulletView =
                Object.Instantiate(_bulletViewTemplate, gun.EndPoint.position, Quaternion.identity);
            BulletPresenter bulletPresenter = new(bullet, bulletView);
            bulletView.Construct(bulletPresenter);
            BulletCreated?.Invoke(gun, bullet);
            Vector3 trajectory = character.CalculateTrajectory(gun, bullet);
            bullet.StartFlight(trajectory);
        }
    }
}
