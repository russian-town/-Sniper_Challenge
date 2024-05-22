using UnityEngine;
using UnityEngine.UI;

namespace Source.Root
{
    public class ScopeView : ViewBase
    {
        [SerializeField] private Image _image;

        public void SetSprite(Sprite sprite)
            => _image.sprite = sprite;

        public void Show()
            => _image.enabled = true;

        public void Hide()
            => _image.enabled = false;
    }
}
