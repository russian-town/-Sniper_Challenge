using Source.Codebase.Domain;
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

        public void Create(GunType gunType, Transform parent)
        {
            GunConfig config = _staticDataService.GetGunConfig(gunType);
            GunView template = config.Template;
            Gun gun = new(config);
            GunView view = Object.Instantiate(template, parent);
            view.SetLocalRotation(parent.localRotation);
            GunPresenter gunPresenter = new(gun, view, config, _bulletFactory);
            view.Construct(gunPresenter);
        }
    }
}
