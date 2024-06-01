using System.Collections;
using UnityEngine;

namespace Source.Root
{
    public class CriminalPresenter : IPresenter
    {
        private readonly Criminal _criminal;
        private readonly CriminalView _view;
        private readonly GameLoopService _gameLoopService;
        private readonly ShooterService _shooterService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly HudUpdateService _hudUpdateService;

        private Coroutine _find;
        private Coroutine _shoot;
        private bool _playerIsFinded;

        public CriminalPresenter(
            Criminal criminal,
            CriminalView view,
            GameLoopService gameLoopService,
            ShooterService shooterService, 
            ICoroutineRunner coroutineRunner,
            HudUpdateService hudUpdateService)
        {
            _criminal = criminal;
            _view = view;
            _gameLoopService = gameLoopService;
            _shooterService = shooterService;
            _coroutineRunner = coroutineRunner;
            _hudUpdateService = hudUpdateService;
        }

        public void Enable()
        {
            _view.DamageRecived += OnDamageRecived;
            _criminal.Died += OnDied;
            _criminal.DamageProcessed += OnDamageProcessed;
            _gameLoopService.PlayerDetected += OnPlayerDetected;
            _view.Shot += OnShot;
        }

        public void Disable()
        {
            _view.DamageRecived -= OnDamageRecived;
            _criminal.Died -= OnDied;
            _criminal.DamageProcessed -= OnDamageProcessed;
            _gameLoopService.PlayerDetected -= OnPlayerDetected;
        }

        private void OnDamageRecived(float damage, Vector3 point)
            => _criminal.TakeDamage(damage, point);

        private void OnPlayerDetected(Transform sniper)
        {
            if(_playerIsFinded)
                return;

            _view.FindSniper();
            _criminal.SetTarget(sniper);

            if(_find != null)
                _coroutineRunner.StopCoroutine(_find);

            _find = _coroutineRunner.StartCoroutine(Find(sniper));
        }

        private IEnumerator Find(Transform sniper)
        {
            yield return new WaitForSeconds(2f);
            _playerIsFinded = true;
            _view.LookAtSniper(sniper);

            if (_shoot != null)
                _coroutineRunner.StopCoroutine(_shoot);

            _shoot = _coroutineRunner.StartCoroutine(Shoot(sniper));
            _find = null;
        }

        private IEnumerator Shoot(Transform sniper)
        {
            while(sniper != null)
            {
                _view.Shoot();
                yield return new WaitForSeconds(2f);
            }
        }

        private void OnShot()
            => _shooterService.CreateBullet(_criminal);

        private void OnDied(Vector3 point)
        {
            _view.PlayDiedAnimation(point);
            _hudUpdateService.ShowDeath();
            _shooterService.UnregistryWeapon(_criminal);
        }

        private void OnDamageProcessed(float damage, Vector3 point)
            => _view.PlayHitAnimation(damage, point);
    }
}
