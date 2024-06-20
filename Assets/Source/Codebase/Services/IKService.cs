using HumanBone = Source.Utils.HumaneBone;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class IKService
    {
        private readonly int _iterations = 10;
        private readonly Transform _target;
        private readonly Transform _gunEnd;
        private readonly Animator _animator;
        private readonly HumanBone[] _bones;
        private readonly Transform[] _bonesTransform;

        public IKService(
            Transform target,
            Transform gunEnd,
            Animator animator,
            HumanBone[] bones)
        {
            _target = target;
            _gunEnd = gunEnd;
            _animator = animator;
            _bones = bones;
            _bonesTransform = new Transform[bones.Length];
        }

        public void Initialize()
        {
            for (int i = 0; i < _bones.Length; i++)
            {
                Transform boneTransform = _animator.GetBoneTransform(_bones[i].Bone);
                _bonesTransform[i] = boneTransform;
            }
        }

        public void UpdateBones(float weight, float angleLimit, float distanceLimit) 
        {
            Vector3 targetPosition = GetTargetPosition(angleLimit, distanceLimit);

            for (int i = 0; i < _iterations; i++)
            {
                for (int j = 0; j < _bonesTransform.Length; j++)
                {
                    float boneWeight = _bones[j].Wight * weight;
                    AimAtTarget(_bonesTransform[j], targetPosition, boneWeight);
                }
            }
        }

        private Vector3 GetTargetPosition(float angleLimit, float distanceLimit)
        {
            Vector3 targetDirection = _target.position - _gunEnd.position;
            Vector3 aimDirection = _gunEnd.forward;
            float blendOut = 0f;
            float targetAngle = Vector3.Angle(targetDirection, aimDirection);

            if (targetAngle > angleLimit)
                blendOut += (targetAngle - angleLimit) / 50f;

            float targetDistance = targetDirection.magnitude;

            if (targetDistance < distanceLimit)
                blendOut += distanceLimit - targetDistance;

            Vector3 direction =
                Vector3.Slerp(targetDirection, aimDirection, blendOut);
            return _gunEnd.position + direction;
        }

        private void AimAtTarget(Transform bone, Vector3 targetPosition, float wieght)
        {
            Vector3 aimDirection = _gunEnd.forward;
            Vector3 targetDirection = targetPosition - _gunEnd.position;
            Quaternion aimTowards =
                Quaternion.FromToRotation(aimDirection, targetDirection);
            Quaternion blendedRotation =
                Quaternion.Slerp(Quaternion.identity, aimTowards, wieght);
            bone.rotation = blendedRotation * bone.rotation;
        }
    }
}
