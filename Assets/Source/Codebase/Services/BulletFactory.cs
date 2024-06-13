using Source.Codebase.Domain;
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

        public void Create(Vector3 gunEndPoint, BulletType bulletType)
        {
            BulletConfig config = _staticDataService.GetBulletConfig(bulletType);
            Bullet bullet = new(config);
            BulletView template = _staticDataService.GetViewTemplate<BulletView>();
            BulletView bulletView =
                Object.Instantiate(template, gunEndPoint, Quaternion.identity);
            BulletPresenter bulletPresenter = new(bullet, bulletView);
            bulletView.Construct(bulletPresenter);
        }
    }
}
