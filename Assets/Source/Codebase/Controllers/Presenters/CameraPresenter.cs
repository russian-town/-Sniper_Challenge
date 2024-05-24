using UnityEngine;

namespace Source.Root
{
    public class CameraPresenter : IPresenter
    {
        private readonly IInput _input;
        private readonly InputConfig _config;
        private readonly InputData _data;
        private readonly Camera _camera;
        private readonly CameraView _cameraView;
        private readonly Transform _transform;

        private float _currentXVelocity;
        private float _currentYVelocity;
        private float _smoothX;
        private float _smoothY;
        private float _lookAngle;
        private float _titleAngle;

        public CameraPresenter(CameraView view, IInput input, InputConfig inputConfig, InputData inputData)
        {
            _cameraView = view;
            _camera = Camera.main;
            _transform = _camera.transform.parent;
            _input = input;
            _config = inputConfig;
            _data = inputData;
        }

        public void Enable()
        => _input.AxisMoved += OnAxisMoved;

        public void Disable()
        => _input.AxisMoved -= OnAxisMoved;

        private void OnAxisMoved(float horizontal, float vertical)
            => RotateCamera(horizontal, vertical);

        private void RotateCamera(float horizontal, float vertical)
        {
            _smoothX =
                    Mathf.SmoothDamp(_smoothX, horizontal, ref _currentXVelocity, _config.TurnSmooth);
            _smoothY =
                Mathf.SmoothDamp(_smoothY, vertical, ref _currentYVelocity, _config.TurnSmooth);
            _lookAngle += _smoothX * _data.SensitivityOfLookAndle;
            _lookAngle = Mathf.Clamp(_lookAngle, _config.MinLookAngle, _config.MaxLookAngle);
            _titleAngle -= _smoothY * _data.SensitivityOfTitleAngle;
            _titleAngle = Mathf.Clamp(_titleAngle, _config.MinTitleAngle, _config.MaxTitleAngle);
            Vector3 offSet = Quaternion.AngleAxis(_lookAngle, Vector3.up) * Vector3.one;
            _transform.position = _cameraView.TargetPosition + offSet + _cameraView.OffSet;
            _transform.LookAt(_cameraView.TargetPosition);
            _cameraView.SetRotation(_titleAngle);
        }
    }
}
