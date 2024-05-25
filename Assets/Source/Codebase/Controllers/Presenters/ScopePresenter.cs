using System.Collections;
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
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _speed = 2f;

        private Coroutine _zoomCamera;

        public ScopePresenter(Scope scope, ScopeView scopeView, GameLoopService gameLoopService, ICoroutineRunner coroutineRunner)
        {
            _scope = scope;
            _scopeView = scopeView;
            _gameLoopService = gameLoopService;
            _camera = Camera.main;
            _coroutineRunner = coroutineRunner;
        }

        public void Enable()
        {
            _gameLoopService.AimEnter += OnAimEnter;
            _gameLoopService.AimExit += OnAimExit;
        }

        public void Disable()
        {
            _gameLoopService.AimEnter -= OnAimEnter;
            _gameLoopService.AimExit -= OnAimExit;
        }

        private void OnAimEnter(float animationLenht)
        {
            _scopeView.Show();
            StartZoom(animationLenht, ScopeFildOfView);
        }

        private void OnAimExit(float animationLenht)
        {
            _scopeView.Hide();
            StartZoom(animationLenht, DefaultFildOfView);
        }

        private void StartZoom(float animationLenht, float targetFildOfView)
        {
            if (_zoomCamera != null)
                _coroutineRunner.StopCoroutine(_zoomCamera);

            _zoomCamera =
                _coroutineRunner.StartCoroutine(ZoomCamera(animationLenht, targetFildOfView));
        }

        private IEnumerator ZoomCamera(float animationLenht, float targetFildOfView)
        {
            while(!Mathf.Approximately(_camera.fieldOfView, targetFildOfView))
            {
                _camera.fieldOfView =
                    Mathf.MoveTowards(_camera.fieldOfView, targetFildOfView,  animationLenht);
                yield return null;
            }

            _zoomCamera = null;
        }
    }
}
