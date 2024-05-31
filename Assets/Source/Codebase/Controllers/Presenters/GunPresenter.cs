namespace Source.Root
{
    public class GunPresenter : IPresenter
    {
        private readonly GunView _view;
        private readonly Gun _gun;

        public GunPresenter(Gun gun, GunView view, GunConfig config)
        {
            _view = view;
            _gun = gun;
            _gun.SetConfig(config);
            _gun.SetEndPoint(_view.GunEnd);
        }

        public void Enable()
        {
            _gun.Shot += OnShot;
        }

        public void Disable()
        {
            _gun.Shot -= OnShot;
        }

        private void OnShot()
        {
            _view.Shoot();
        }
    }
}
