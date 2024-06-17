using Source.Codebase.Services.Abstract;
using UnityEngine;

namespace Source.Root
{
    public class BulletPresenter : IPresenter
    {
        private readonly Bullet _bullet;
        private readonly BulletView _view;
        private readonly BulletServiceBase _bulletService;

        public BulletPresenter(
            Bullet bullet,
            BulletView view,
            BulletServiceBase bulletService)
        {
            _bullet = bullet;
            _view = view;
            _bulletService = bulletService;
        }

        public void Enable()
        {
            _bulletService.PositionChanged += OnPositionChanged;
            _bulletService.FlightIsOver += OnFlightIsOver;
            _bulletService.Fly(_bullet.Damage);
        }

        public void Disable()
        {
            _bulletService.PositionChanged -= OnPositionChanged;
            _bulletService.FlightIsOver -= OnFlightIsOver;
        } 

        private void OnPositionChanged(Vector3 position)
        {
            Quaternion rotation = Quaternion.LookRotation(position);
            _view.SetPosition(position);
            _view.SetRotation(rotation);
        }

        private void OnFlightIsOver()
            => _view.Destroy();
    }
}
