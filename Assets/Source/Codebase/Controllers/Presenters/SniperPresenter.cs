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
        private readonly HudUpdateService _hudUpdateService;

        private Coroutine _aim;

        public SniperPresenter(Sniper sniper,
            SniperView view,
            IInput input,
            ICoroutineRunner coroutineRunner,
            GameLoopService gameLoopService,
            ShooterService shooterService,
            HudUpdateService hudUpdateService)
        {
            _sniper = sniper;
            _view = view;
            _input = input;
            _coroutineRunner = coroutineRunner;
            _gameLoopService = gameLoopService;
            _shooterService = shooterService;
            _view.Initialize();
            _hudUpdateService = hudUpdateService;
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
            _input.ShootButtonDown += OnShootButtonDown;
            _gameLoopService.CameraRotationChanged += OnCameraRotationChanged;
            _view.DamageRecived += OnDamageRecived;
            _sniper.HealthChanged += OnHealthChanged;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
            _gameLoopService.CameraRotationChanged -= OnCameraRotationChanged;
            _view.DamageRecived -= OnDamageRecived;
            _sniper.HealthChanged -= OnHealthChanged;
        }

        private void OnAimButtonDown()
        {
            if (_aim != null)
                return;

            _aim = _coroutineRunner.StartCoroutine(Aim());
        }

        private void OnShootButtonDown()
        {
            _shooterService.CreateBullet(_sniper);
            _gameLoopService.SniperShoot(_view.TargetOfCriminal);
        }

        private void OnCameraRotationChanged(float angle)
            => _view.UpdateRotation(angle);

        private void OnDamageRecived(float damage, Vector3 point)
            => _sniper.TakeDamage(damage, point);

        private void OnHealthChanged(float value)
            => _hudUpdateService.UpdateHealthBar(value);

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
