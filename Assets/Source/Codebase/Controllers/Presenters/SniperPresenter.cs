using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public class SniperPresenter : IPresenter
    {
        private readonly Sniper _sniper;
        private readonly SniperView _view;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;
        private readonly ShooterService _shooterService;
        private Coroutine _aim;

        public SniperPresenter(Sniper sniper, SniperView view, IInput input, ICoroutineRunner coroutineRunner, GameLoopService gameLoopService, ShooterService shooterService)
        {
            _sniper = sniper;
            _view = view;
            _input = input;
            _coroutineRunner = coroutineRunner;
            _gameLoopService = gameLoopService;
            _shooterService = shooterService;
            _view.Initialize();
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
            _input.ShootButtonDown += OnShootButtonDown;
            _gameLoopService.CameraRotationChanged += OnCameraRotationChanged;
            _sniper.TrajectoryDetermined += OnTrajectoryDetermined;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
            _gameLoopService.CameraRotationChanged -= OnCameraRotationChanged;
            _sniper.TrajectoryDetermined -= OnTrajectoryDetermined;
        }

        private void OnAimButtonDown()
        {
            if (_aim != null)
                return;

            _aim = _coroutineRunner.StartCoroutine(Aim());
        }

        private void OnShootButtonDown()
            => _sniper.CalculateTrajectory();

        private void OnCameraRotationChanged(float angle)
            => _view.UpdateRotation(angle);

        private void OnTrajectoryDetermined(Ray ray)
        {
            _shooterService.CreateBullet(_sniper, ray);
            _gameLoopService.SniperShoot(_view.transform);
        }

        private IEnumerator Aim()
        {
            if(_sniper.InAim == false)
            {
                float animationLenht = _view.EnterToAim();
                _gameLoopService.EnterToAim(animationLenht);
                yield return new WaitForSeconds(animationLenht);
                _sniper.EnterToAim();
            }
            else
            {
                float animationLenht = _view.ExitOfAim();
                _gameLoopService.ExitOfAim(animationLenht);
                yield return new WaitForSeconds(animationLenht);
                _sniper.ExitOfAim();
            }

            _aim = null;
        }
    }
}
