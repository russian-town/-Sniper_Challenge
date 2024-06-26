using System;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class IKService
    {
        public event Action<Transform, Transform, Transform> HandsTargetsInitialized;

        public void InitializeTargets(
            Transform rightHandTarget,
            Transform leftHandTarget,
            Transform gun)
            => HandsTargetsInitialized?.Invoke(
                rightHandTarget,
                leftHandTarget,
                gun);
    }
}
