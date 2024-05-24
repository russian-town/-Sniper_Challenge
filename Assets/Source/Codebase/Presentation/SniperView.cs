using System;
using UnityEngine;

namespace Source.Root
{
    public class SniperView : ViewBase
    {
        public const string AimParameter = "Aim";
        public const string SpeedMultiplierParameter = "SpeedMultiplier";

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _aimEnterClip;
        [SerializeField] private AnimationClip _aimExitClip;

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

        internal void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}
