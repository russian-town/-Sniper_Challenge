using System;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Root
{
    public class CriminalView : ViewBase
    {
        private const string FindParameter = "Find";
        private const string ShootParameter = "Shoot";

        [SerializeField] private BodyPart[] _bodyParts;
        [SerializeField] private Transform _transform;

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public Transform DamageViewPoint { get; private set; }
        [field: SerializeField] public Transform GunPoint { get; private set; }

        public event Action<float, Vector3> DamageRecived;
        public event Action Shot;

        public void PlayHitAnimation(float damage, Vector3 point) { }

        public void PlayDiedAnimation()
            => Animator.enabled = false;

        public void ProcessCalculatedDamage(float calculatedDamage, Vector3 point)
            => DamageRecived?.Invoke(calculatedDamage, point);

        public void FindSniper()
            => Animator.SetBool(FindParameter, true);

        public void CancelFind()
            => Animator.SetBool(FindParameter, false);

        public void Shoot()
            => Animator.SetTrigger(ShootParameter);

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
