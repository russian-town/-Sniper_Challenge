using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Source.Root
{
    public class SniperView : ViewBase, IDamageable
    {
        private const string AimParameter = "Aim";
        private const string SpeedMultiplierParameter = "SpeedMultiplier";

        [SerializeField] private AnimationClip _aimEnterClip;
        [SerializeField] private AnimationClip _aimExitClip;
        [SerializeField] private Transform _transform;
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private TwoBoneIKConstraint _rightHandConstraint;
        [SerializeField] private TwoBoneIKConstraint _leftHandCanstraint;
        [SerializeField] private MultiParentConstraint _gunIdleConstraint;
        [SerializeField] private MultiParentConstraint _gunAimConstraint;

        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform TargetOfCriminal { get; private set; }
        [field: SerializeField] public Transform GunPoint { get; private set; }
        [field: SerializeField] public Transform Center { get; private set; }
        [field: SerializeField] public LayerMask AimMask { get; private set; }

        public event Action<float, Vector3> DamageRecived;

        public float EnterToAim()
        {
            Animator.SetBool(AimParameter, true);
            return _aimEnterClip.length / Animator.GetFloat(SpeedMultiplierParameter);
        }

        public float ExitOfAim()
        {
            Animator.SetBool(AimParameter, false);
            return _aimExitClip.length / Animator.GetFloat(SpeedMultiplierParameter);
        }

        public void TakeDamage(float damage, Vector3 point)
            => DamageRecived?.Invoke(damage, point);

        public void InitializeHandsConstraint(
            Transform rightHandTarget,
            Transform leftHandTarget,
            Transform gun)
        {
            _rightHandConstraint.data.target = rightHandTarget;
            _leftHandCanstraint.data.target = leftHandTarget;
            _gunIdleConstraint.data.constrainedObject = gun;
            _gunAimConstraint.data.constrainedObject = gun;
            _rigBuilder.enabled = true;
        }
    }
}
