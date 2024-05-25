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
        private readonly GameLoopService _gameLoopService;

        private Coroutine _aim;
        private Coroutine _shoot;

        public SniperPresenter(Sniper sniper, SniperView view, IInput input, ICoroutineRunner coroutineRunner, GameLoopService gameLoopService)
        {
            _sniper = sniper;
            _view = view;
            _input = input;
            _coroutineRunner = coroutineRunner;
            _gameLoopService = gameLoopService;
        }

        public void Enable()
        {
            _input.AimButtonDown += OnAimButtonDown;
            _gameLoopService.Shot += OnShot;
        }

        public void Disable()
        {
            _input.AimButtonDown -= OnAimButtonDown;
            _gameLoopService.Shot -= OnShot;
        }

        private void OnAimButtonDown()
        {
            if (_aim != null || _shoot != null)
                return;

            _aim = _coroutineRunner.StartCoroutine(Aim());
        }

        private void OnShot()
        {
            if (_sniper.InAim)
            {
                if (_shoot != null)
                    return;

                _shoot = _coroutineRunner.StartCoroutine(Shoot());
                return;
            }

            _sniper.Shoot();
            _view.Shoot();
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(_view.Shoot());
            _sniper.Shoot();
            _gameLoopService.ExitOfAim();
            yield return new WaitForSeconds(_view.ExitOfAim());
            _sniper.ExitOfAim();
            _shoot = null;
        }

        private IEnumerator Aim()
        {
            if(_sniper.InAim == false)
            {
                float animationLenht = _view.EnterToAim();
                _gameLoopService.EnterToAim(animationLenht);
                yield return new WaitForSeconds(animationLenht);
                _sniper.EnterToAim();
            }
            else
            {
                _gameLoopService.ExitOfAim();
                yield return new WaitForSeconds(_view.ExitOfAim());
                _sniper.ExitOfAim();
            }

            _aim = null;
        }
    }
}
