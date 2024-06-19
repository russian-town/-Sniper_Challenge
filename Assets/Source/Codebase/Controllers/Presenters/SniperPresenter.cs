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
        private readonly IStaticDataService _staticDataService;
        private readonly ShootService _shootService;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;
        private readonly AchievementFactory _achievementFactory;
        private readonly HudUpdateService _hudUpdateService;
        private readonly GunFactory _gunFactory;
        private readonly Transform _gunEnd;
        private readonly int _iterations = 10;

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
            _input = input;
            _gameLoopService = gameLoopService;
            _view.Initialize();
            _achievementFactory = achievementFactory;
            _hudUpdateService = hudUpdateService;
            GunConfig config = _staticDataService.GetGunConfig(GunType.Rifle);
            _gunFactory = gunFactory;
            _gunFactory.Create(config, _view.GunPoint, _shootService);
            _gunEnd = _gunFactory.GetGunEnd();
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
            _input.ShootButtonDown += OnShootButtonDown;
            _view.DamageRecived += OnDamageRecived;
            _sniper.HealthChanged += OnHealthChanged;
            _sniper.Died += OnDied;
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

            Vector3 targetPosition = GetTargetPosition();

            for (int i = 0; i < _iterations; i++)
            {
                for (int j = 0; j < _view.BonesTransform.Count; j++)
                {
                    float boneWeight = _view.HumanBones[j].Wight * _sniper.IKWeight;
                    AimAtTarget(_view.BonesTransform[j], targetPosition, boneWeight);
                }
            }
        }

        private Vector3 GetTargetPosition()
        {
            Vector3 targetDirection = _view.Center.position - _gunEnd.position;
            Vector3 aimDirection = _gunEnd.forward;
            float blendOut = 0f;
            float targetAngle = Vector3.Angle(targetDirection, aimDirection);

            if(targetAngle >  _sniper.AngleLimit)
                blendOut += (targetAngle - _sniper.AngleLimit) / 50f;

            float targetDistance = targetDirection.magnitude;

            if (targetDistance < _sniper.DistanceLimit)
                blendOut += _sniper.DistanceLimit - targetDistance;

            Vector3 direction =
                Vector3.Slerp(targetDirection, aimDirection, blendOut);
            return _gunEnd.position + direction;
        }

        private void AimAtTarget(Transform bone, Vector3 targetPosition, float wieght)
        {
            Vector3 aimDirection = _gunEnd.forward;
            Vector3 targetDirection = targetPosition - _gunEnd.position;
            Quaternion aimTowards =
                Quaternion.FromToRotation(aimDirection, targetDirection);
            Quaternion blendedRotation =
                Quaternion.Slerp(Quaternion.identity, aimTowards, wieght);
            bone.rotation = blendedRotation * bone.rotation;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
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
