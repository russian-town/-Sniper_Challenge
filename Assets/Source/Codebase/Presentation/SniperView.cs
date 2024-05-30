using System;
using UnityEngine;

namespace Source.Root
{
    public class SniperView : ViewBase, IDamageable
    {
        private const string AimParameter = "Aim";
        private const string SpeedMultiplierParameter = "SpeedMultiplier";

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _aimEnterClip;
        [SerializeField] private AnimationClip _aimExitClip;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _target;

        private float _startRotation;

        public event Action<float, Vector3> DamageRecived;

        public void Initialize()
            => _startRotation = _transform.eulerAngles.y;

        public float EnterToAim()
        {
            _animator.SetBool(AimParameter, true);
            return _aimEnterClip.length / _animator.GetFloat(SpeedMultiplierParameter);
        }

        public float ExitOfAim()
        {
            _animator.SetBool(AimParameter, false);
            return _aimExitClip.length / _animator.GetFloat(SpeedMultiplierParameter);
        }

        public void UpdateRotation(float angle)
        {
            Vector3 euler = _transform.eulerAngles;
            euler.y = angle + _startRotation;
            _transform.eulerAngles = euler;
        }

        public void TakeDamage(float damage, Vector3 point)
            => DamageRecived?.Invoke(damage, point);
    }
}
