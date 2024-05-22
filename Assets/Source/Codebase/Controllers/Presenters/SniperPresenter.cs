using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public class SniperPresenter : IPresenter
    {
        private readonly Sniper _sniper;
        private readonly SniperView _view;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IInput _input;
        private readonly AimingService _aimingService;

        private Coroutine _aim;

        public SniperPresenter(Sniper sniper, SniperView view, IInput input, ICoroutineRunner coroutineRunner, AimingService aimingService)
        {
            _sniper = sniper;
            _view = view;
            _input = input;
            _coroutineRunner = coroutineRunner;
            _aimingService = aimingService;
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
        }

        private void OnAimButtonDown()
        {
            if (_aim != null)
                _coroutineRunner.StopCoroutine(_aim);

            _aim = _coroutineRunner.StartCoroutine(Aim());
        }

        private IEnumerator Aim()
        {
            yield return new WaitForSeconds(_view.AimComeIn());
            _aim = null;
            _aimingService.Aim();
        }
    }
}
