using UnityEngine;
using DG.Tweening;

namespace Source.Root
{
    public class CameraView : ViewBase
    {
        private readonly float _duration = .35f;

        [SerializeField] private Transform _transform;

        public void SetRotation(float angle)
            => _transform.localRotation = Quaternion.Euler(angle, 0f, 0f);

        public void PlayRecoil(float recoilForce)
        {
            if (recoilForce == 0f)
                return;

            _transform.DOPunchRotation(_transform.right * recoilForce, _duration);
        }
    }
}