using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Root
{
    public class ScopeView : ViewBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _crossHair;

        private readonly float _shootCrossMultiplaer = 2f;

        private Vector2 _startCrossHairSize;

        public void SetSprite(Sprite sprite)
            => _image.sprite = sprite;

        public void Initialize()
            => _startCrossHairSize = _crossHair.sizeDelta;

        public void Show()
        {
            _crossHair.gameObject.SetActive(false);
            _image.enabled = true;
        }

        public void Hide()
        {
            _image.enabled = false;
            _crossHair.gameObject.SetActive(true);
        }

        public void Shoot()
        {
            _crossHair.sizeDelta *= _shootCrossMultiplaer;
            _crossHair.DOSizeDelta(_startCrossHairSize, .5f); 
        }
    }
}
