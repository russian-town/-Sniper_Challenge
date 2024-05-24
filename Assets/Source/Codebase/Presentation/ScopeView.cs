using UnityEngine;
using UnityEngine.UI;

namespace Source.Root
{
    public class ScopeView : ViewBase
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _aim;

        public void SetSprite(Sprite sprite)
            => _image.sprite = sprite;

        public void Show()
        {
            _aim.gameObject.SetActive(false);
            _image.enabled = true;
        }

        public void Hide()
        {
            _image.enabled = false;
            _aim.gameObject.SetActive(true);
        }
    }
}
