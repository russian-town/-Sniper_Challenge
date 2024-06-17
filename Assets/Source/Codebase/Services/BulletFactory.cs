using Source.Codebase.Domain;
using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Root
{
    public class BulletFactory
    {
        private readonly IStaticDataService _staticDataService;

        public BulletFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Create(
            Vector3 gunEndPoint,
            BulletType bulletType,
            BulletServiceBase bulletService)
        {
            BulletConfig config = _staticDataService.GetBulletConfig(bulletType);
            Bullet bullet = new(config);
            BulletView template = _staticDataService.GetViewTemplate<BulletView>();
            BulletView bulletView =
                Object.Instantiate(template, gunEndPoint, Quaternion.identity);
            BulletPresenter bulletPresenter = new(bullet, bulletView, bulletService);
            bulletView.Construct(bulletPresenter);
        }
    }
}
