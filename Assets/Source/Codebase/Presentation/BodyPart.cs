using UnityEngine;

namespace Source.Root
{
    public class BodyPart : MonoBehaviour, IDamageable, IBodyPart
    {
        [SerializeField] private float _damageMultiplier;
        [SerializeField] private CriminalView _view;
        [SerializeField] private BodyPartName _name;
        [SerializeField] private Rigidbody _rigidbody;

        public BodyPartName Name => _name;

        public void ProcessCalculatedDamage(float damage, Vector3 point)
        {
            float calculatedDamage = damage * _damageMultiplier;
            _rigidbody.AddForceAtPosition(point * damage, point);
            _view.ProcessCalculatedDamage(calculatedDamage, point);
        }
    }
}
