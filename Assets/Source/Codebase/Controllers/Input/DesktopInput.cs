using System;
using UnityEngine;

namespace Source.Root
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        public event Action AimButtonDown;
        public event Action AimButtonUp;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                AimButtonDown?.Invoke();
        }
    }
}
