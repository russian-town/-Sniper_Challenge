using System;
using UnityEngine;

namespace Source.Root
{
    public class CriminalView : ViewBase, IDamageable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _bodyParts;
        [SerializeField] private Rigidbody _body;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _gunEnd;
        [SerializeField] private Transform _target;

        public event Action<float, Vector3> DamageRecived;

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

        public void LookAtSniper(Transform sniper)
            => _transform.LookAt(sniper.position);
    }
}
