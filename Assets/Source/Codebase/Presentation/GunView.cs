﻿using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Source.Root
{
    public class GunView : ViewBase
    {
        [SerializeField] private ParticleSystem _muzzleFlash;

        [field: SerializeField] public Transform Transform {  get; private set; }
        [field: SerializeField] public Transform GunEnd {  get; private set; }
        [field: SerializeField] public Transform LeftHandTarget { get; private set; }
        [field: SerializeField] public Transform RightHandTarget { get; private set; }

        public void SetLocalRotation(Quaternion localRotation)
            => transform.localRotation = localRotation;

        public void SetLocalPosition(Vector3 localPosition)
            => transform.localPosition = localPosition;

        public void Shoot()
            => _muzzleFlash.Play();
    }
}