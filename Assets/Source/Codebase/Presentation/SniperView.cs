using UnityEngine;

namespace Source.Root
{
    public class SniperView : ViewBase
    {
        public const string AimParameter = "Aim";

        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationClip _aimEnterClip;
        [SerializeField] private AnimationClip _aimExitClip;

        public float AimComeIn()
        {
            _animator.SetBool(AimParameter, true);
            return _aimEnterClip.length;
        }

        public float AimComeOut()
        {
            _animator.SetBool(AimParameter, false);
            return _aimExitClip.length;
        }
    }
}
