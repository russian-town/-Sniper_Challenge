using System;
using UnityEngine;

namespace Source.Root
{
    public abstract class ViewBase : MonoBehaviour, IViewBase
    {
        private IPresenter _presenter;

        public void Construct(IPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException(nameof(presenter));

            gameObject.SetActive(false);
            _presenter = presenter;
            gameObject.SetActive(true);
        }

        public void Destroy() =>
            Destroy(gameObject);

        private void OnEnable()
            => _presenter?.Enable();

        private void LateUpdate()
            => _presenter?.LateUpdate(Time.deltaTime);

        private void OnDisable()
            => _presenter?.Disable();
    }
}
