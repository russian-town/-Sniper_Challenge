using System;

namespace Source.Codebase.Services.Abstract
{
    public interface IShootService
    {
        public event Action<BulletServiceBase> Shot;

        public void Shoot(BulletServiceBase bulletService);
    }
}
