using System;
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

        private Coroutine _find;
        private bool _playerIsFinded;

        public CriminalPresenter(
            Criminal criminal,
            CriminalView view,
            GameLoopService gameLoopService,
            ShooterService shooterService, 
            ICoroutineRunner coroutineRunner)
        {
            _criminal = criminal;
            _view = view;
            _gameLoopService = gameLoopService;
            _shooterService = shooterService;
            _coroutineRunner = coroutineRunner;
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
            {
                _view.Shoot();
                return;
            }

            _view.FindSniper();
            _criminal.SetTarget(sniper);

            if(_find != null)
                _coroutineRunner.StopCoroutine(_find);

            _coroutineRunner.StartCoroutine(Find(sniper));
        }

        private IEnumerator Find(Transform sniper)
        {
            yield return new WaitForSeconds(2f);
            _playerIsFinded = true;
            _view.LookAtSniper(sniper);
            _view.Shoot();
        }

        private void OnShot()
            => _shooterService.CreateBullet(_criminal);

        private void OnDied(Vector3 point)
            => _view.PlayDiedAnimation(point);

        private void OnDamageProcessed(float damage, Vector3 point)
            => _view.PlayHitAnimation(damage, point);
    }
}
