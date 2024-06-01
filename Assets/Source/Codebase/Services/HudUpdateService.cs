using System;
using DG.Tweening;
using UnityEngine;

namespace Source.Root
{
    public class HudUpdateService
    {
        private CanvasGroup _bloodOverlay;
        private CanvasGroup _skull;

        public event Action<float> HealthChanged;

        public void SetBloodOverlayImage(CanvasGroup bloodOverlay)
        {
            _bloodOverlay = bloodOverlay;
            _bloodOverlay.alpha = 0f;
        }

        public void SetSkullImage(CanvasGroup skull) 
        {
            _skull = skull;
            _skull.alpha = 0f;
        }

        public void UpdateHealthBar(float health)
        {
            HealthChanged?.Invoke(health);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_bloodOverlay.DOFade(1f, .35f));
            sequence.Append(_bloodOverlay.DOFade(0f, .5f));
            sequence.Play();
        }

        public void ShowDeath()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_skull.DOFade(1f, .35f));
            sequence.AppendInterval(.5f);
            sequence.Append(_skull.transform.DOMoveY(Screen.height, 1f).SetEase(Ease.OutBack)).Join(_skull.DOFade(0f, .5f));
            sequence.Play();
            _skull.alpha = 0;
        }
    }
}
