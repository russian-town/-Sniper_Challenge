using System;
using System.Collections.Generic;
using UnityEngine;
using HumanBone = Source.Utils.HumaneBone;

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
        [SerializeField] private List<HumanBone> _humanBones = new();

        private readonly List<Transform> _bonesTransform = new();

        private float _startRotation;

        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public Transform TargetOfCriminal { get; private set; }
        [field: SerializeField] public Transform GunPoint { get; private set; }
        [field: SerializeField] public Transform Center { get; private set; }
        [field: SerializeField] public LayerMask AimMask { get; private set; }

        public IReadOnlyList<HumanBone> HumanBones => _humanBones;
        public IReadOnlyList<Transform> BonesTransform => _bonesTransform;

        public event Action<float, Vector3> DamageRecived;

        public void Initialize()
        {
            _startRotation = _transform.eulerAngles.y;

            foreach (var humanBone in _humanBones)
            {
                Transform boneTransform = _animator.GetBoneTransform(humanBone.Bone);
                _bonesTransform.Add(boneTransform);
            }
        } 

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
