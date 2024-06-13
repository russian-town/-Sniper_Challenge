using UnityEngine;

namespace Source.Root
{
    public class GunView : ViewBase
    {
        [SerializeField] private ParticleSystem _muzzleFlash;

        [field: SerializeField] public Transform GunEnd {  get; private set; }

        public void SetLocalRotation(Quaternion localRotation)
            => transform.localRotation = localRotation;

        public void Shoot()
            => _muzzleFlash.Play();
    }
}