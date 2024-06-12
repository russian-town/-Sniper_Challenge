using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Root
{
    public class AchievementView : ViewBase, IAchievementView
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;
        [SerializeField] private float _duration = .5f;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private RectTransform _rectTransform;

        public void ResetAnimation()
        {
            _canvasGroup.alpha = 0;
            _rectTransform.position = Vector3.zero;
        }

        public void SetName(string name)
            => _name.text = name;

        public void SetScore(float score)
        {
            string scoreText = "+ " + score.ToString();
            _score.text = scoreText;
        }

        public async UniTask ShowAnimation()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_canvasGroup.DOFade(1f, .35f));
            sequence.AppendInterval(.5f);
            sequence.Append(
                _rectTransform.DOMoveY(
                    Screen.height, 1f).SetEase(
                    Ease.OutBack)).Join(_canvasGroup.DOFade(0f, .5f));
            await UniTask.WaitUntil(() => sequence.IsActive() == false);
        }

        public void SetParent(RectTransform parent)
            => _rectTransform.SetParent(parent, false);

        public void SetSprite(Sprite sprite)
            => _image.sprite = sprite;
    }
}
