using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Root
{
    public class CriminalPresenter : IPresenter, IStateMachine
    {
        private readonly Criminal _criminal;
        private readonly CriminalView _view;
        private readonly GameLoopService _gameLoopService;
        private readonly ShooterService _shooterService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly HudUpdateService _hudUpdateService;
        private readonly Dictionary<Type, State> _states;

        private State _activeState;
        private Transform _sniper;

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
            State idleState = new IdleState(this);
            State detectingState = new DetectingState(this, view, _sniper);
            State lookingState = new LookingState(this, view);
            State shootingState = new ShootingState(this, view);
            _states = new Dictionary<Type, State>
            {
                { idleState.GetType(), idleState },
                { detectingState.GetType(), detectingState },
                { lookingState.GetType(), lookingState },
                { shootingState.GetType(), shootingState }
            };
            Enter<IdleState>();
        }

        public event Action SniperDetected;

        public void Enable()
        {
            _view.DamageRecived += OnDamageRecived;
            _criminal.Died += OnDied;
            _criminal.DamageProcessed += OnDamageProcessed;
            _gameLoopService.SniperShot += OnSniperShot;
            _gameLoopService.SniperDied += OnSniperDied;
            _view.Shot += OnShot;
        }

        public void Disable()
        {
            _view.DamageRecived -= OnDamageRecived;
            _criminal.Died -= OnDied;
            _criminal.DamageProcessed -= OnDamageProcessed;
            _gameLoopService.SniperShot -= OnSniperShot;
            _gameLoopService.SniperDied -= OnSniperDied;
            _activeState?.Exit();
        }

        public void Enter<T>() where T : State
        {
            _activeState?.Exit();
            State state = _states[typeof(T)];
            _activeState = state;
            state.Enter();
        }

        private void OnDamageRecived(float damage, Vector3 point)
            => _criminal.TakeDamage(damage, point);

        private void OnSniperShot(Transform sniper)
        {
            _sniper = sniper;
            _criminal.SetTarget(sniper);
            SniperDetected?.Invoke();
        }

        private void OnSniperDied()
            => Enter<IdleState>();

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
