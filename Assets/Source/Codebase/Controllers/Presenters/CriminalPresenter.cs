using System;
using System.Collections.Generic;
using Source.Codebase.Services;
using UnityEngine;

namespace Source.Root
{
    public class CriminalPresenter : IPresenter, IStateMachine
    {
        private readonly Criminal _criminal;
        private readonly CriminalView _view;
        private readonly ShootService _shootService;
        private readonly GameLoopService _gameLoopService;
        private readonly DamageBarFactory _damageBarFactory;
        private readonly IKService _ikService;
        private readonly Dictionary<Type, State> _states;
        private readonly Transform _target;

        private State _activeState;

        public CriminalPresenter(
            Criminal criminal,
            CriminalView view,
            Transform target,
            GameLoopService gameLoopService,
            DamageBarFactory damageBarFactory,
            GunFactory gunFactory,
            GunConfig gunConfig)
        {
            _criminal = criminal;
            _view = view;
            _target = target;
            _shootService = new();
            _gameLoopService = gameLoopService;
            _damageBarFactory = damageBarFactory;
            gunFactory.Create(gunConfig, _view.GunPoint, _shootService);
            _ikService =
                new(_target, gunFactory.GetGunEnd(), _view.Animator, _view.Bones);
            State idleState = new IdleState(this);
            State detectingState = new DetectingState(this);
            State lookingState = new LookingState(this, view);
            State shootingState = new ShootingState(this, _ikService, view);
            _states = new Dictionary<Type, State>
            {
                { idleState.GetType(), idleState },
                { detectingState.GetType(), detectingState },
                { lookingState.GetType(), lookingState },
                { shootingState.GetType(), shootingState }
            };
            Enter<IdleState>();
        }

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

        public void LateUpdate(float tick) 
            => _activeState?.Update();

        public void Disable()
        {
            _view.DamageRecived -= OnDamageRecived;
            _criminal.HealthChanged -= OnHealthChanged;
            _criminal.Died -= OnDied;
            _criminal.DamageProcessed -= OnDamageProcessed;
            _gameLoopService.SniperShot -= OnSniperShot;
            _gameLoopService.SniperDied -= OnSniperDied;
            _view.Shot -= OnShot;
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

        private void OnSniperShot()
            => Enter<LookingState>();

        private void OnSniperDied()
            => Enter<IdleState>();

        private void OnShot()
        {
            if (_target == null)
                throw new Exception($"Null reference exception {nameof(_target)}");

            CriminalBulletService bulletService = new(_target);
            _shootService.Shoot(bulletService);
        }

        private void OnDied()
        {
            _activeState?.Exit();
            _view.PlayDiedAnimation();
        }

        private void OnDamageProcessed(float damage, Vector3 point)
            => _view.PlayHitAnimation(damage, point);
    }
}
