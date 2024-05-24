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
        }

        public void Disable()
        {
        }
    }
}
