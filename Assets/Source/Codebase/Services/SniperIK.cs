using UnityEngine;

namespace Source.Root
{
    public class SniperIK : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField][Range(0f, 1f)] private float _ikWeight = 1f;
        [SerializeField] private Transform _leftHandPoint;

        private void OnAnimatorIK()
        {
            if (_animator == null)
                return;

            _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _ikWeight);
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _ikWeight);
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandPoint.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandPoint.rotation);
            _animator.SetLookAtWeight(_ikWeight);
        }
    }
}
