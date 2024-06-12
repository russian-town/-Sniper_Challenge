using UnityEngine;

namespace Source.Root
{
    public class ScopePresenter : IPresenter
    {
        private const float ScopeFildOfView = 15f;
        private const float DefaultFildOfView = 60f;

        private readonly Scope _scope;
        private readonly ScopeView _scopeView;
        private readonly GameLoopService _gameLoopService;
        private readonly Camera _camera;

        public ScopePresenter(Scope scope, ScopeView scopeView, GameLoopService gameLoopService)
        {
            _scope = scope;
            _scopeView = scopeView;
            _gameLoopService = gameLoopService;
            _camera = Camera.main;
            _scopeView.Initialize();
        }

        public void Enable()
        {
            _gameLoopService.AimEnter += OnAimEnter;
            _gameLoopService.AimExit += OnAimExit;
            _gameLoopService.Shot += OnShot;
        }

        public void Disable()
        {
            _gameLoopService.AimEnter -= OnAimEnter;
            _gameLoopService.AimExit -= OnAimExit;
            _gameLoopService.Shot -= OnShot;
        }

        private void OnShot()
            => _scopeView.Shoot();

        private void OnAimEnter()
        {
            _scopeView.Show();
            _camera.fieldOfView = ScopeFildOfView;
        }

        private void OnAimExit()
        {
            _scopeView.Hide();
            _camera.fieldOfView = DefaultFildOfView;
        }
    }
}
