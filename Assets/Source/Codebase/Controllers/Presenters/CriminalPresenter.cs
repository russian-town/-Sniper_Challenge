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
            _view.DamageRecived += OnDamageRecived;
            _criminal.Died += OnDied;
            _criminal.DamageProcessed += OnDamageProcessed;
        }

        public void Disable()
        {
            _view.DamageRecived -= OnDamageRecived;
            _criminal.Died -= OnDied;
            _criminal.DamageProcessed -= OnDamageProcessed;
        }

        private void OnDamageRecived(float damage, Vector3 point)
            => _criminal.TakeDamage(damage, point);

        private void OnDied(Vector3 point)
            => _view.PlayDiedAnimation(point);

        private void OnDamageProcessed(float damage, Vector3 point)
            => _view.PlayHitAnimation(damage, point);
    }
}
