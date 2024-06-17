using Source.Codebase.Domain;
using Source.Codebase.Services.Abstract;
using Source.Root;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class GunFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly BulletFactory _bulletFactory;

        public GunFactory(
            IStaticDataService staticDataService,
            BulletFactory bulletFactory)
        {
            _staticDataService = staticDataService;
            _bulletFactory = bulletFactory;
        }

        public void Create(
            GunType gunType,
            Transform parent,
            IShootService shootService)
        {
            GunConfig config = _staticDataService.GetGunConfig(gunType);
            GunView template = config.Template;
            Gun gun = new(config);
            GunView view = Object.Instantiate(template, parent);
            view.SetLocalPosition(new Vector3(0.192f, 0.196f, -0.036f));
            view.SetLocalRotation(Quaternion.Euler(new Vector3(315, 100, 90)));
            GunPresenter gunPresenter =
                new(gun, view, config, _bulletFactory, shootService);
            view.Construct(gunPresenter);
        }
    }
}
