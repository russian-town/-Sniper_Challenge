using System;
using UnityEngine;

namespace Source.Root
{
    public class CriminalView : ViewBase
    {
        private const string FindParameter = "Find";
        private const string ShootParameter = "Shoot";

        [SerializeField] private Animator _animator;
        [SerializeField] private BodyPart[] _bodyParts;
        [SerializeField] private Transform _transform;

        private Transform _sniper;

        [field: SerializeField] public Transform DamageViewPoint { get; private set; }
        [field: SerializeField] public Transform GunPoint { get; private set; }

        public event Action<float, Vector3> DamageRecived;
        public event Action Shot;

        public void PlayHitAnimation(float damage, Vector3 point)
        {
        }

        public void PlayDiedAnimation()
        {
            _animator.enabled = false;
        }

        public void ProcessCalculatedDamage(float calculatedDamage, Vector3 point)
            => DamageRecived?.Invoke(calculatedDamage, point);

        public void FindSniper()
            => _animator.SetBool(FindParameter, true);

        public void Shoot()
            => _animator.SetTrigger(ShootParameter);

        public void SetTarget(Transform sniper)
            => _sniper = sniper;

        public void LookAtSniper()
        {
            _animator.SetBool(FindParameter, false);
            _transform.LookAt(_sniper.position);
        }

        public void CallShotEvent()
            => Shot?.Invoke();

        public void UpdateHealth(float currentHealth)
        {
            foreach (var bodyPart in _bodyParts)
            {
                bodyPart.SetHealth(currentHealth);
            }
        }
    }
}
