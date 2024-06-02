using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Source.Root
{
    public class SniperPresenter : IPresenter
    {
        private readonly Sniper _sniper;
        private readonly SniperView _view;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;
        private readonly ShooterService _shooterService;
        private readonly HudUpdateService _hudUpdateService;

        public SniperPresenter(Sniper sniper,
            SniperView view,
            IInput input,
            GameLoopService gameLoopService,
            ShooterService shooterService,
            HudUpdateService hudUpdateService)
        {
            _sniper = sniper;
            _view = view;
            _input = input;
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
            _sniper.Died += OnDied;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
            _gameLoopService.CameraRotationChanged -= OnCameraRotationChanged;
            _view.DamageRecived -= OnDamageRecived;
            _sniper.HealthChanged -= OnHealthChanged;
            _sniper.Died -= OnDied;
        }

        private async void OnAimButtonDown()
            => await Aim();

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

        private void OnDied(Vector3 vector)
            => _gameLoopService.CallEventOfSniperDied();

        private async UniTask Aim()
        {
            if(_sniper.InAim == false)
            {
                float animationLenht = _view.EnterToAim();
                await UniTask.Delay(TimeSpan.FromSeconds(animationLenht));
                _sniper.EnterToAim();
                _gameLoopService.EnterToAim(animationLenht);
            }
            else
            {
                float animationLenht = _view.ExitOfAim();
                _gameLoopService.ExitOfAim(animationLenht);
                await UniTask.Delay(TimeSpan.FromSeconds(animationLenht));
                _sniper.ExitOfAim();
            }
        }
    }
}
