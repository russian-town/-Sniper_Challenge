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
        private readonly AimingService _aimingServices;
        private readonly Camera _camera;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _zoomCamera;

        public ScopePresenter(Scope scope, ScopeView scopeView, AimingService aimingServices, ICoroutineRunner coroutineRunner)
        {
            _scope = scope;
            _scopeView = scopeView;
            _aimingServices = aimingServices;
            _camera = Camera.main;
            _coroutineRunner = coroutineRunner;
        }

        public void Enable()
        {
            _aimingServices.AimEnter += OnAimEnter;
            _aimingServices.AimExit += OnAimExit;
        }

        public void Disable()
        {
            _aimingServices.AimEnter -= OnAimEnter;
            _aimingServices.AimExit -= OnAimExit;
        }

        private void OnAimEnter(float animationLenht)
        {
            _scopeView.Show();

            if (_zoomCamera != null)
                _coroutineRunner.StopCoroutine(_zoomCamera);

            _zoomCamera =
                _coroutineRunner.StartCoroutine(ZoomCamera(animationLenht));
        }

        private void OnAimExit()
        {
            _scopeView.Hide();

            if (_zoomCamera != null)
                _coroutineRunner.StopCoroutine(_zoomCamera);

            _camera.fieldOfView = DefaultFildOfView;
        }

        private IEnumerator ZoomCamera(float animationLenht)
        {
            while(!Mathf.Approximately(_camera.fieldOfView, ScopeFildOfView))
            {
                _camera.fieldOfView =
                    Mathf.Lerp(_camera.fieldOfView, ScopeFildOfView,  animationLenht);
                yield return null;
            }

            _zoomCamera = null;
        }
    }
}
