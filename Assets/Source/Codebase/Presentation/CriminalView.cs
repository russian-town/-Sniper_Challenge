using System;
using DG.Tweening;
using UnityEngine;

namespace Source.Root
{
    public class CriminalView : ViewBase, IDamageable
    {
        private const string FindParameter = "Find";
        private const string ShootParameter = "Shoot";

        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _bodyParts;
        [SerializeField] private Rigidbody _body;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _gunEnd;
        [SerializeField] private Transform _target;

        public event Action<float, Vector3> DamageRecived;
        public event Action PlayerFinded;
        public event Action Shot;

        public void PlayHitAnimation(float damage, Vector3 point)
            => _body.AddForceAtPosition(Vector3.one * damage * 100f, point);

        public void PlayDiedAnimation(Vector3 point)
        {
            foreach (var rigidbody in _bodyParts)
                rigidbody.isKinematic = false;

            _animator.enabled = false;
        }

        public void TakeDamage(float damage, Vector3 point)
            => DamageRecived(damage, point);

        public void FindSniper()
            => _animator.SetBool(FindParameter, true);

        public void Shoot()
            => _animator.SetTrigger(ShootParameter);

        public void LookAtSniper(Transform sniper)
        {
            _animator.SetBool(FindParameter, false);
            _transform.LookAt(sniper.position);
        }

        public void CallShotEvent()
            => Shot?.Invoke();
    }
}
