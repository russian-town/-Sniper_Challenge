using System;
using UnityEngine;

namespace Source.Root
{
    public class SniperView : ViewBase
    {
        private const string AimParameter = "Aim";
        private const string SpeedMultiplierParameter = "SpeedMultiplier";
        private const string ShootParameter = "Shoot";

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _aimEnterClip;
        [SerializeField] private AnimationClip _aimExitClip;
        [SerializeField] private AnimationClip _shootClip;
        [SerializeField] private Transform _transform;

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

        public float Shoot()
        {
            _animator.SetTrigger(ShootParameter);
            return _shootClip.length;
        }
    }
}
