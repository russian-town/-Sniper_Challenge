using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Root
{
    public class AchievementView : ViewBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;
        [SerializeField] private float _duration = .5f;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private RectTransform _rectTransform;

        private Sequence _sequence;

        private readonly CancellationTokenSource _tokenSource = new();

        public void ResetAnimation()
        {
            _canvasGroup.alpha = 0;
            _rectTransform.localScale = Vector3.zero;
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
            _sequence = DOTween.Sequence();
            _sequence.Append(_canvasGroup.DOFade(1f, .35f)).Join(
                _rectTransform.DOScale(Vector3.one, .35f));
            _sequence.AppendInterval(.5f);
            _sequence.Append(_canvasGroup.DOFade(0f, .5f)).Join(
                _rectTransform.DOScale(Vector3.zero, .5f));
            await UniTask.WaitUntil(
                () => _sequence.IsActive() == false,
                cancellationToken: _tokenSource.Token);
            Destroy();
        }

        public void SetPosition(Vector3 localPosition)
            => _rectTransform.localPosition = localPosition;

        public void SetSprite(Sprite sprite)
            => _image.sprite = sprite;
    }
}
