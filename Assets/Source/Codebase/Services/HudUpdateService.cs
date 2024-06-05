using System;
using DG.Tweening;
using UnityEngine;

namespace Source.Root
{
    public class HudUpdateService
    {
        private CanvasGroup _bloodOverlay;

        public event Action<float> HealthChanged;

        public void SetBloodOverlayImage(CanvasGroup bloodOverlay)
        {
            _bloodOverlay = bloodOverlay;
            _bloodOverlay.alpha = 0f;
        }

        public void UpdateHealthBar(float health)
        {
            HealthChanged?.Invoke(health);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_bloodOverlay.DOFade(1f, .35f));
            sequence.Append(_bloodOverlay.DOFade(0f, .5f));
            sequence.Play();
        }
    }
}
