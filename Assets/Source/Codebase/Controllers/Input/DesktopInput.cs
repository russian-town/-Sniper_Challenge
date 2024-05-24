using System;
using UnityEngine;

namespace Source.Root
{
    public class DesktopInput : MonoBehaviour, IInput
    {
        private const string MouseXAxis = "Mouse X";
        private const string MouseYAxis = "Mouse Y";

        public event Action AimButtonDown;
        public event Action AimButtonUp;
        public event Action<float, float> AxisMoved;

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
                AimButtonDown?.Invoke();

            float horizontal = Input.GetAxis(MouseXAxis);
            float vertical = Input.GetAxis(MouseYAxis);

            AxisMoved?.Invoke(horizontal, vertical);
        }
    }
}
