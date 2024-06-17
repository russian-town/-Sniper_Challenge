using System;
using Cysharp.Threading.Tasks;
using Source.Codebase.Domain;
using Source.Codebase.Services;
using UnityEngine;

namespace Source.Root
{
    public class SniperPresenter : IPresenter
    {
        private readonly Sniper _sniper;
        private readonly SniperView _view;
        private readonly ShootService _shootService;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;
        private readonly AchievementFactory _achievementFactory;
        private readonly HudUpdateService _hudUpdateService;
        private readonly GunFactory _gunFactory;

        public SniperPresenter(
            Sniper sniper,
            SniperView view,
            IInput input,
            GameLoopService gameLoopService,
            HudUpdateService hudUpdateService,
            AchievementFactory achievementFactory,
            GunFactory gunFactory)
        {
            _sniper = sniper;
            _view = view;
            _shootService = new();
            _input = input;
            _gameLoopService = gameLoopService;
            _view.Initialize();
            _achievementFactory = achievementFactory;
            _hudUpdateService = hudUpdateService;
            _gunFactory = gunFactory;
            _gunFactory.Create(GunType.Rifle, _view.GunPoint, _shootService);
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
        {
            if (_sniper.InAim == false)
                await EnterToAim();
            else
                ExitOfAim();
        }

        private async void OnShootButtonDown()
        {
            SniperBulletService bulletService = new(_achievementFactory, _sniper.InAim);
            _shootService.Shoot(bulletService);
            _gameLoopService.SniperShoot(_view.TargetOfCriminal, GunType.Rifle);
            await UniTask.WaitForSeconds(.35f);
            ExitOfAim();
        }

        private void OnCameraRotationChanged(float angle)
            => _view.UpdateRotation(angle);

        private void OnDamageRecived(float damage, Vector3 point)
            => _sniper.TakeDamage(damage, point);

        private void OnHealthChanged(float value)
            => _hudUpdateService.UpdateHealthBar(value);

        private void OnDied()
            => _gameLoopService.CallEventOfSniperDied();

        private async UniTask EnterToAim()
        {
            float animationLenht = _view.EnterToAim();
            await UniTask.Delay(TimeSpan.FromSeconds(animationLenht));
            _sniper.EnterToAim();
            _gameLoopService.EnterToAim();
        }

        private void ExitOfAim()
        {
            _view.ExitOfAim();
            _gameLoopService.ExitOfAim();
            _sniper.ExitOfAim();
        }
    }
}
