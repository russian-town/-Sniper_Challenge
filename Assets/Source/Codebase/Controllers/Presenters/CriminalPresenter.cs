using UnityEngine;

namespace Source.Root
{
    public class CriminalPresenter : IPresenter
    {
        private readonly Criminal _criminal;
        private readonly CriminalView _view;

        public CriminalPresenter(Criminal criminal, CriminalView view)
        {
            _criminal = criminal;
            _view = view;
        }

        public void Enable()
        {
            _criminal.Died += OnDied;
            _criminal.DamageRecived += OnDamageRecived;
            _view.Damage += _criminal.TakeDamage;
        }

        public void Disable()
        {
            _criminal.Died -= OnDied;
            _criminal.DamageRecived -= OnDamageRecived;
            _view.Damage -= _criminal.TakeDamage;
        }

        private void OnDied(Vector3 point)
            => _view.PlayDiedAnimation(point);

        private void OnDamageRecived(float damage, Vector3 point)
            => _view.PlayHitAnimation(damage, point);
    }
}
