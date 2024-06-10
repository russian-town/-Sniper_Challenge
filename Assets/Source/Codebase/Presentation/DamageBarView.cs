using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Source.Root
{
    public class DamageBarView : ViewBase
    {
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _transform;

        public void SetDamageText(string text)
            => _damageText.text = text;

        public async UniTask ShotAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(1, .35f)).Join(_transform.DOMoveY(3, .35f));
            sequence.Append(_canvasGroup.DOFade(0, .35f));
            await new WaitUntil(() => sequence.IsActive() == false);
        }
    }
}
