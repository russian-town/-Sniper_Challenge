using Source.Codebase.Services.Abstract;
using Source.Root;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class GunFactory
    {
        private readonly BulletFactory _bulletFactory;

        private GunView _gunView;

        public GunFactory(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
        }

        public void Create(
            GunConfig config,
            Transform parent,
            IShootService shootService)
        { 
            GunView template = config.Template;
            Gun gun = new(config);
            GunView view = Object.Instantiate(template, parent);
            _gunView = view;
            view.SetLocalPosition(config.LocalPosition);
            view.SetLocalRotation(config.LocalRotation);
            GunPresenter gunPresenter =
                new(gun, view, config, _bulletFactory, shootService);
            view.Construct(gunPresenter);
        }

        public Transform GetGunEnd()
            => _gunView.GunEnd;
    }
}
