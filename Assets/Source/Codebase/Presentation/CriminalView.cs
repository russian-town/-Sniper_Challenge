using System;
using UnityEngine;

namespace Source.Root
{
    public class CriminalView : ViewBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _bodyParts;
        [SerializeField] private Rigidbody _body;

        public event Action<float, Vector3> Damage;

        public void PlayHitAnimation(float damage, Vector3 point)
            => _body.AddForceAtPosition(Vector3.one * damage * 100f, point);

        public void PlayDiedAnimation(Vector3 point)
        {
            foreach (var rigidbody in _bodyParts)
                rigidbody.isKinematic = false;
        }
    }
}
