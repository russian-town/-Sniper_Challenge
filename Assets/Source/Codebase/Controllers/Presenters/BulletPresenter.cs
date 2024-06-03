using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Source.Root
{
    public class BulletPresenter : IPresenter
    {
        private readonly Bullet _bullet;
        private readonly BulletView _view;

        public BulletPresenter(Bullet bullet, BulletView view)
        {
            _bullet = bullet;
            _view = view;
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

        private async void OnFlewOut(Vector3 point)
            => await Fly(point);

        private async UniTask Fly(Vector3 point)
        {
            Vector3 target;
            _view.SetDirection(point);
            CancellationTokenSource cancellationToken = new();

            while (_bullet.GoalAchieved(point) == false)
            {
                float step = Time.deltaTime * _bullet.FlightSpeed;
                target = Vector3.MoveTowards(_bullet.CurrentPosition, point, step);
                _bullet.ChangePosition(target);
                _bullet.UpdateResults();
                await UniTask.Yield(cancellationToken.Token);
            }

            cancellationToken.Cancel();
            _view.Destroy();
        }
    }
}
