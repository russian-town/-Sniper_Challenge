using Source.Codebase.Services.Abstract;

namespace Source.Root
{
    public class GunPresenter : IPresenter
    {
        private readonly GunView _view;
        private readonly Gun _gun;
        private readonly GunConfig _config;
        private readonly BulletFactory _bulletFactory;
        private readonly IShootService _shootService;

        public GunPresenter(
            Gun gun,
            GunView view,
            GunConfig config,
            BulletFactory bulletFactory,
            IShootService shootService)
        {
            _view = view;
            _gun = gun;
            _config = config;
            _bulletFactory = bulletFactory;
            _shootService = shootService;
        }

        public void Enable()
            => _shootService.Shot += OnShot;

        public void LateUpdate(float tick) { }

        public void Disable()
            => _shootService.Shot -= OnShot;

        private void OnShot(BulletServiceBase bulletService)
        {
            bulletService.SetGunEnd(_view.GunEnd);
            bulletService.SetGunRange(_gun.Range);
            _bulletFactory.Create(
                _view.GunEnd.position,
                _config.BulletType,
                bulletService);
        }
    }
}
