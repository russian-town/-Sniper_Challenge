using Source.Codebase.Domain;
using Source.Codebase.Services;
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
        private readonly DamageBarFactory _damageBarFactory;
        private readonly Dictionary<Type, State> _states;
        private readonly GunFactory _gunFactory;

        private State _activeState;
        private Transform _sniper;

        public CriminalPresenter(
            Criminal criminal,
            CriminalView view,
            GameLoopService gameLoopService,
            DamageBarFactory damageBarFactory,
            GunFactory gunFactory)
        {
            _criminal = criminal;
            _view = view;
            _gameLoopService = gameLoopService;
            _damageBarFactory = damageBarFactory;
            _gunFactory = gunFactory;
            _gunFactory.Create(GunType.Pistol, _view.GunPoint);
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
            _criminal.HealthChanged += OnHealthChanged;
            _gameLoopService.SniperShot += OnSniperShot;
            _gameLoopService.SniperDied += OnSniperDied;
            _view.Shot += OnShot;
        }

        public void Disable()
        {
            _view.DamageRecived -= OnDamageRecived;
            _criminal.HealthChanged -= OnHealthChanged;
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
        {
            _damageBarFactory.Create(damage, _view.DamageViewPoint.position);
            _criminal.TakeDamage(damage, point);
        }

        private void OnHealthChanged(float currentHealth)
            => _view.UpdateHealth(currentHealth);

        private void OnSniperShot(Transform sniper)
        {
            _sniper = sniper;
            _criminal.SetTarget(sniper);
            _view.SetTarget(sniper);
            SniperDetected?.Invoke();
        }

        private void OnSniperDied()
            => Enter<IdleState>();

        private void OnShot() { }

        private void OnDied()
        {
            _activeState?.Exit();
            _view.PlayDiedAnimation();
        }

        private void OnDamageProcessed(float damage, Vector3 point)
        {
            _view.PlayHitAnimation(damage, point);
        }
    }
}
