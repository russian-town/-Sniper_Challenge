using System;
using UnityEngine;

namespace Source.Root
{
    public class GunPresenter : IPresenter
    {
        private readonly IInput _input;
        private readonly GunView _view;
        private readonly Gun _gun;
        private readonly GameLoopService _gameLoopService;

        public GunPresenter(IInput input, Gun gun, GunView view, GameLoopService gameLoopService, GunConfig config)
        {
            _input = input;
            _view = view;
            _gun = gun;
            _gameLoopService = gameLoopService;
            _gun.SetConfig(config);
            _gun.SetGunEnd(_view.GunEnd);
        }

        public void Enable()
        {
            _input.ShootButtonDown += OnShootButtonDown;
            _gun.Shot += OnShot;
        }

        public void Disable()
        {
            _input.ShootButtonDown -= OnShootButtonDown;
            _gun.Shot -= OnShot;
        }

        private void OnShootButtonDown()
        {
            _view.Shoot();
            _gun.Shoot();
        }

        private void OnShot(RaycastHit[] raycastHits)
            => _gameLoopService.Shoot(_view.GunEnd, raycastHits);
    }
}
