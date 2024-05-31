using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public class BulletPresenter : IPresenter
    {
        private readonly Bullet _bullet;
        private readonly BulletView _view;
        private readonly ICoroutineRunner _coroutineRunner;

        private Coroutine _fly;

        public BulletPresenter(Bullet bullet, BulletView view, ICoroutineRunner coroutineRunner)
        {
            _bullet = bullet;
            _view = view;
            _coroutineRunner = coroutineRunner;
        }

        public void Enable()
        {
            _bullet.PositionChanged += OnPositionChanged;
            _bullet.FlewOut += OnFlewOut;
        }

        public void Disable()
        {
            _bullet.PositionChanged -= OnPositionChanged;
            _bullet.FlewOut -= OnFlewOut;
        }

        private void OnPositionChanged(Vector3 position)
            => _view.SetPosition(position);

        private void OnFlewOut(Vector3 point)
        {
            if (_fly != null)
                _coroutineRunner.StopCoroutine(_fly);

            _fly = _coroutineRunner.StartCoroutine(Fly(point));
        }

        private IEnumerator Fly(Vector3 point)
        {
            Vector3 target;
            _view.SetDirection(point);

            while (Vector3.Distance(_bullet.CurrentPosition, point) > 0)
            {
                float step = Time.deltaTime * _bullet.FlightSpeed;
                target = Vector3.MoveTowards(_bullet.CurrentPosition, point, step);
                _bullet.ChangePosition(target);
                yield return null;
            }

            _fly = null;
            _view.Destroy();
        }
    }
}
