using System;
using Cysharp.Threading.Tasks;
using Source.Codebase.Domain;
using Source.Codebase.Services;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Source.Root
{
    public class SniperPresenter : IPresenter
    {
        private readonly Sniper _sniper;
        private readonly SniperView _view;
        private readonly IStaticDataService _staticDataService;
        private readonly ShootService _shootService;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;
        private readonly AchievementFactory _achievementFactory;
        private readonly HudUpdateService _hudUpdateService;
        private readonly IKService _ikService;

        public SniperPresenter(
            Sniper sniper,
            SniperView view,
            IStaticDataService staticDataService,
            IInput input,
            GameLoopService gameLoopService,
            HudUpdateService hudUpdateService,
            AchievementFactory achievementFactory,
            GunFactory gunFactory)
        {
            _sniper = sniper;
            _view = view;
            _staticDataService = staticDataService;
            _shootService = new();
            _ikService = new();
            _input = input;
            _gameLoopService = gameLoopService;
            _achievementFactory = achievementFactory;
            _hudUpdateService = hudUpdateService;
            GunConfig config = _staticDataService.GetGunConfig(GunType.Rifle);
            gunFactory.Create(
                config,
                _view.GunPoint,
                _shootService,
                _ikService);
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
            _input.ShootButtonDown += OnShootButtonDown;
            _view.DamageRecived += OnDamageRecived;
            _sniper.HealthChanged += OnHealthChanged;
            _sniper.Died += OnDied;
            _ikService.HandsTargetsInitialized += OnHandsTargetsInitialized;
            _gameLoopService.CallEventOfSniperCreated(_view.transform);
        }

        public void LateUpdate(float tick)
        {
            Vector2 center = new(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(center);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _view.AimMask))
            {
                float step = tick * _sniper.AimSpeed;
                _view.Center.position =
                    Vector3.Lerp(_view.Center.position, hitInfo.point, step);
            }
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
            _view.DamageRecived -= OnDamageRecived;
            _sniper.HealthChanged -= OnHealthChanged;
            _sniper.Died -= OnDied;
            _ikService.HandsTargetsInitialized -= OnHandsTargetsInitialized;
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
            _gameLoopService.SniperShoot(GunType.Rifle);
            await UniTask.WaitForSeconds(.35f);
            ExitOfAim();
        }

        private void OnDamageRecived(float damage, Vector3 point)
            => _sniper.TakeDamage(damage, point);

        private void OnHealthChanged(float value)
            => _hudUpdateService.UpdateHealthBar(value);

        private void OnDied()
            => _gameLoopService.CallEventOfSniperDied();

        private void OnHandsTargetsInitialized(
            Transform rightHandTarget,
            Transform leftHandTarget,
            Transform gun)
            => _view.InitializeHandsConstraint(
                rightHandTarget,
                leftHandTarget,
                gun);

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
