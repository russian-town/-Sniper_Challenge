using System;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action AimButtonDown;
    public event Action AimButtonUp;

    public void Update()
    {
        if(Input.GetMouseButtonDown(2))
            AimButtonDown?.Invoke();

        if(Input.GetMouseButtonUp(2))
            AimButtonUp?.Invoke();
    }
}
