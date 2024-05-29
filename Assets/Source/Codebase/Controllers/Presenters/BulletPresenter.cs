using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public class BulletPresenter : IPresenter
    {
        private readonly Bullet _bullet;
        private readonly BulletView _view;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly RaycastHit[] _raycastHits;

        private Coroutine _move;

        public BulletPresenter(Bullet bullet, BulletView view, ICoroutineRunner coroutineRunner, RaycastHit[] raycastHits)
        {
            _bullet = bullet;
            _view = view;
            _coroutineRunner = coroutineRunner;
            _raycastHits = raycastHits;
        }

        public void Enable()
        {
            _bullet.PositionChanged += OnPositionChanged;
            StartMove();
        }

        public void Disable()
            => _bullet.PositionChanged -= OnPositionChanged;

        private void OnPositionChanged(Vector3 position)
            => _view.SetPosition(position);

        private void StartMove()
        {
            if (_move != null)
                _coroutineRunner.StopCoroutine(_move);

            _move =
                _coroutineRunner.StartCoroutine(Move(_raycastHits));
        }

        private IEnumerator Move(RaycastHit[] raycastHits)
        {
            if(raycastHits.Length == 0)
                yield break;

            foreach (RaycastHit hit in raycastHits)
            {
                _view.SetDirection(hit.point);

                while (Vector3.Distance(_bullet.CurrentPosition, hit.point) > 0f)
                {
                    float step = Time.deltaTime * _bullet.FlightSpeed;
                    Vector3 newPosition =
                        Vector3.MoveTowards(_bullet.CurrentPosition, hit.point, step);
                    _bullet.ChangePosition(newPosition);
                    yield return null;
                }

                _bullet.Attack(hit);
            }

            _view.Destroy();
            _move = null;
        }
    }
}
