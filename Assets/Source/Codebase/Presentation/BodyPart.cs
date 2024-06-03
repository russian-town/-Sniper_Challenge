using UnityEngine;

namespace Source.Root
{
    public class BodyPart : MonoBehaviour, IDamageable, IBodyPart
    {
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private CriminalView _view;
        [SerializeField] private BodyPartName _name;
        [SerializeField] private Rigidbody _rigidbody;

        private float _currentHealth;

        public BodyPartName Name => _name;

        public void TakeDamage(float damage, Vector3 point)
        {
            float calculatedDamage = damage * _damageMultiplier;
            _rigidbody.AddForceAtPosition(point * damage, point);
            _view.ProcessCalculatedDamage(calculatedDamage, point);
        }

        public void SetHealth(float currentHealth)
            => _currentHealth = currentHealth;

        public bool CheckDead(float damage)
            => _currentHealth - damage * _damageMultiplier <= 0;
    }
}
