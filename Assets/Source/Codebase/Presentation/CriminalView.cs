using System;
using TMPro;
using UnityEngine;

namespace Source.Root
{
    public class CriminalView : ViewBase
    {
        private const string FindParameter = "Find";
        private const string ShootParameter = "Shoot";

        [SerializeField] private Animator _animator;
        [SerializeField] private Collider[] _bodyParts;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _gunEnd;

        public event Action<float, Vector3> DamageRecived;
        public event Action Shot;

        public void PlayHitAnimation(float damage, Vector3 point)
        {
        }

        public void PlayDiedAnimation(Vector3 point)
        {
            _animator.enabled = false;
        }

        public void ProcessCalculatedDamage(float calculatedDamage, Vector3 point)
            => DamageRecived?.Invoke(calculatedDamage, point);

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
