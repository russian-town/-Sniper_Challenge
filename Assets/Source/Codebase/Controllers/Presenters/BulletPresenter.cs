using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public class BulletPresenter : IPresenter
    {
        private readonly Bullet _bullet;
        private readonly BulletView _view;
        private readonly Camera _camera;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _move;

        public BulletPresenter(Bullet bullet, BulletView view, ICoroutineRunner coroutineRunner)
        {
            _bullet = bullet;
            _view = view;
            _camera = Camera.main;
            _coroutineRunner = coroutineRunner;
        }

        public void Enable()
        {
            _bullet.PositionChanged += OnPositionChanged;

            if (_move != null)
                _coroutineRunner.StopCoroutine(_move);

            _move =
                _coroutineRunner.StartCoroutine(Move(CalculatePosition()));
        }

        public void Disable()
        {
            _bullet.PositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged(Vector3 position)
            => _view.SetPosition(position);

        private Vector3 CalculatePosition()
        {
            Ray ray = new(_camera.transform.position, _camera.transform.forward);

            if(Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
                return hitInfo.point;

            Vector3 targetPosition
                = _view.transform.forward * _bullet.MaxDistance + _bullet.StartPosition;

            return targetPosition;
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            _view.SetDirection(targetPosition);

            while (Vector3.Distance(_bullet.CurrentPosition, targetPosition) > 0f)
            {
                float step = Time.deltaTime * _bullet.FlightSpeed;
                Vector3 newPosition =
                    Vector3.MoveTowards(_bullet.CurrentPosition, targetPosition, step);
                _bullet.ChangePosition(newPosition);
                yield return null;
            }

            _view.Destroy();
            _move = null;
        }
    }
}
