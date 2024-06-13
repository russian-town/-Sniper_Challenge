namespace Source.Root
{
    public class GunPresenter : IPresenter
    {
        private readonly GunView _view;
        private readonly Gun _gun;
        private readonly GunConfig _config;
        private readonly BulletFactory _bulletFactory;

        public GunPresenter(Gun gun,
            GunView view,
            GunConfig config,
            BulletFactory bulletFactory)
        {
            _view = view;
            _gun = gun;
            _config = config;
            _bulletFactory = bulletFactory;
        }

        public void Enable() { }

        public void Disable() { }

        public void Shoot()
        {
            _view.Shoot();
            _bulletFactory.Create(_view.GunEnd.position, _config.BulletType);
        }
    }
}
