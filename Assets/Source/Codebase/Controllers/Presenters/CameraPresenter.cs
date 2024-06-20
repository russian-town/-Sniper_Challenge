using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Root
{
    public class CameraPresenter : IPresenter
    {
        private readonly IInput _input;
        private readonly CameraConfig _config;
        private readonly InputData _data;
        private readonly Camera _camera;
        private readonly CameraView _view;
        private readonly Transform _transform;
        private readonly GameLoopService _gameLoopService;
        private readonly StaticDataService _staticDataService;

        private Transform _target;
        private float _currentXVelocity;
        private float _currentYVelocity;
        private float _smoothX;
        private float _smoothY;
        private float _lookAngle;
        private float _titleAngle;

        public CameraPresenter(
            CameraView view,
            IInput input,
            CameraConfig cameraConfig,
            InputData inputData,
            GameLoopService gameLoopService,
            StaticDataService staticDataService)
        {
            _view = view;
            _camera = Camera.main;
            _transform = _camera.transform.parent;
            _input = input;
            _config = cameraConfig;
            _data = inputData;
            _gameLoopService = gameLoopService;
            _staticDataService = staticDataService;
        }

        public void Enable()
        {
            _input.AxisMoved += OnAxisMoved;
            _gameLoopService.Shot += OnShot;
            _gameLoopService.SniperCreated += OnSniperCreated;
        }

        public void LateUpdate(float tick) { }

        public void Disable()
        {
            _input.AxisMoved -= OnAxisMoved;
            _gameLoopService.Shot -= OnShot;
            _gameLoopService.SniperCreated -= OnSniperCreated;
        } 

        private void OnAxisMoved(float horizontal, float vertical)
            => RotateCamera(horizontal, vertical);

        private void OnShot(GunType gunType)
        {
            GunConfig gunConfig = _staticDataService.GetGunConfig(gunType);
            _view.PlayRecoil(gunConfig.RecoilForce);
        }

        private void OnSniperCreated(Transform sniper)
            => _target = sniper;

        private void RotateCamera(float horizontal, float vertical)
        {
            if (_target == null)
                return;

            _smoothX =
                    Mathf.SmoothDamp(_smoothX, horizontal, ref _currentXVelocity, _config.TurnSmooth);
            _smoothY =
                Mathf.SmoothDamp(_smoothY, vertical, ref _currentYVelocity, _config.TurnSmooth);
            _lookAngle += _smoothX * _data.SensitivityOfLookAndle;
            _lookAngle = Mathf.Clamp(_lookAngle, _config.MinLookAngle, _config.MaxLookAngle);
            _titleAngle -= _smoothY * _data.SensitivityOfTitleAngle;
            _titleAngle = Mathf.Clamp(_titleAngle, _config.MinTitleAngle, _config.MaxTitleAngle);
            Vector3 offSet = Quaternion.AngleAxis(_lookAngle + 180f, Vector3.up) * Vector3.one;
            _transform.position = _target.position + offSet;
            _transform.LookAt(_target);
            _view.SetRotation(_titleAngle);
        }
    }
}
