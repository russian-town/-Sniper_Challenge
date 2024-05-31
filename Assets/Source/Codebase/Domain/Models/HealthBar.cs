using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Root
{
    public class HealthBar
    {
        private readonly float _startValue;
        
        private float _currentValue;

        public HealthBar(float startValue)
        {
            _startValue = startValue;
        }

        public event Action<float> ValueUpdated;

        public void UpdateValue(float value)
        {
            _currentValue = value;
            float normalizeValue = _currentValue / _startValue;
            ValueUpdated?.Invoke(normalizeValue);
        }
    }
}
