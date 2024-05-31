using UnityEngine;
using UnityEngine.UI;

namespace Source.Root
{
    public class HealthBarView : ViewBase
    {
        [SerializeField] private Slider _slider;

        public void UpdateValue(float normalizeValue)
            => _slider.value = normalizeValue;
    }
}
