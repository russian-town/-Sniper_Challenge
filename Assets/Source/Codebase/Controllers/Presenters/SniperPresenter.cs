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
        private readonly AchievementFactory _achievementFactory;

        public SniperPresenter(Sniper sniper,
            SniperView view,
            IInput input,
            GameLoopService gameLoopService,
            ShooterService shooterService,
            HudUpdateService hudUpdateService,
            AchievementFactory achievementFactory)
        {
            _sniper = sniper;
            _view = view;
            _input = input;
            _gameLoopService = gameLoopService;
            _shooterService = shooterService;
            _view.Initialize();
            _hudUpdateService = hudUpdateService;
            _achievementFactory = achievementFactory;
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
            _input.ShootButtonDown += OnShootButtonDown;
            _gameLoopService.CameraRotationChanged += OnCameraRotationChanged;
            _view.DamageRecived += OnDamageRecived;
            _sniper.HeadShot += OnHeadShot;
            _sniper.HipfireShot += OnHipfireShot;
            _sniper.ThroughCoverHit += OnThroughCoverHit;
            _sniper.MultiKill += OnMultiKill;
            _sniper.HealthChanged += OnHealthChanged;
            _sniper.Died += OnDied;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
            _gameLoopService.CameraRotationChanged -= OnCameraRotationChanged;
            _view.DamageRecived -= OnDamageRecived;
            _sniper.HeadShot -= OnHeadShot;
            _sniper.HipfireShot -= OnHipfireShot;
            _sniper.ThroughCoverHit -= OnThroughCoverHit;
            _sniper.MultiKill -= OnMultiKill;
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

        private void OnShootButtonDown()
        {
            _shooterService.CreateBullet(_sniper);
            _gameLoopService.SniperShoot(_view.TargetOfCriminal);
            ExitOfAim();
        }

        private void OnCameraRotationChanged(float angle)
            => _view.UpdateRotation(angle);

        private void OnDamageRecived(float damage, Vector3 point)
            => _sniper.TakeDamage(damage, point);


        private void OnMultiKill(int killCount)
        {
            if (killCount < 3)
                _achievementFactory.Create(AchievementsType.DoubleKill);
            else
                _achievementFactory.Create(AchievementsType.MultiKill);
        }

        private void OnThroughCoverHit()
            => _achievementFactory.Create(AchievementsType.ThroughCoverHit);

        private void OnHipfireShot()
        {
            _achievementFactory.Create(AchievementsType.HipfireShot);
            Debug.Log("HipFire");
        }

        private void OnHeadShot()
        {
            _achievementFactory.Create(AchievementsType.HeadShot);
            Debug.Log("Headshot");
        } 

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
