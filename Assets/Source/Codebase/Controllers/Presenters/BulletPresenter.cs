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

        private void OnFlewOut(RaycastHit[] raycastHits)
        {
            if (_fly != null)
                _coroutineRunner.StopCoroutine(_fly);

            _fly =
                _coroutineRunner.StartCoroutine(Fly(raycastHits));
        }

        private IEnumerator Fly(RaycastHit[] raycastHits)
        {
            if(raycastHits.Length  == 0)
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
            _fly = null;
        }
    }
}
