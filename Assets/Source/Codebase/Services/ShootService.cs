using System;
using Source.Codebase.Services.Abstract;

namespace Source.Codebase.Services
{
    public class ShootService : IShootService
    {
        public event Action<BulletServiceBase> Shot;

        public void Shoot(BulletServiceBase bulletService)
            => Shot?.Invoke(bulletService);
    }
}
