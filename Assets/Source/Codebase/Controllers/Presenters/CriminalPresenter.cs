using UnityEngine;

namespace Source.Root
{
    public class CriminalPresenter : IPresenter
    {
        private readonly Criminal _criminal;
        private readonly CriminalView _view;
        private readonly GameLoopService _gameLoopService;
        private readonly ShooterService _shooterService;

        public CriminalPresenter(
            Criminal criminal,
            CriminalView view,
            GameLoopService gameLoopService,
            ShooterService shooterService)
        {
            _criminal = criminal;
            _view = view;
            _gameLoopService = gameLoopService;
            _shooterService = shooterService;
            _criminal.SetStartPosition(_view.transform.position);
        }

        public void Enable()
        {
            _view.DamageRecived += OnDamageRecived;
            _criminal.Died += OnDied;
            _criminal.DamageProcessed += OnDamageProcessed;
            _gameLoopService.PlayerDetected += OnPlayerDetected;
            _criminal.TrajectoryDetermined += OnTrajectoryDetermined;
        }

        public void Disable()
        {
            _view.DamageRecived -= OnDamageRecived;
            _criminal.Died -= OnDied;
            _criminal.DamageProcessed -= OnDamageProcessed;
            _gameLoopService.PlayerDetected -= OnPlayerDetected;
            _criminal.TrajectoryDetermined -= OnTrajectoryDetermined;
        }

        private void OnDamageRecived(float damage, Vector3 point)
            => _criminal.TakeDamage(damage, point);

        private void OnPlayerDetected(Transform transform)
        {
            _criminal.SetTarget(transform);
            _criminal.CalculateTrajectory();
        } 

        private void OnDied(Vector3 point)
            => _view.PlayDiedAnimation(point);

        private void OnDamageProcessed(float damage, Vector3 point)
            => _view.PlayHitAnimation(damage, point);

        private void OnTrajectoryDetermined(Ray ray)
            => _shooterService.CreateBullet(_criminal, ray);
    }
}
