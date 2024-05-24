using System;
using UnityEngine;

namespace Source.Root
{
    public class MobileInpute : MonoBehaviour, IInput
    {
        public event Action AimButtonDown;
        public event Action ShootButtonDown;
        public event Action<float, float> AxisMoved;
    }
}
