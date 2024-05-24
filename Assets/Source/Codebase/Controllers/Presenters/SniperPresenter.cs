using System;
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
            _input.ShootButtonDown += OnShootButtonDown;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _input.ShootButtonDown -= OnShootButtonDown;
        }

        private void OnAimButtonDown()
        {
            if (_aim != null)
                return;

            _aim = _coroutineRunner.StartCoroutine(Aim());
        }

        private void OnShootButtonDown()
        {
            
        }

        private IEnumerator Aim()
        {
            if(_sniper.InAim == false)
            {
                float animationLenht = _view.EnterToAim();
                _aimingService.EnterToAim(animationLenht);
                yield return new WaitForSeconds(animationLenht);
                _sniper.EnterToAim();
            }
            else
            {
                _aimingService.ExitOfAim();
                yield return new WaitForSeconds(_view.ExitOfAim());
                _sniper.ExitOfAim();
            }

            _aim = null;
        }
    }
}
