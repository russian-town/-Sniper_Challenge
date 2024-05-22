using UnityEngine;

namespace Source.Root
{
    public class ScopePresenter : IPresenter
    {
        private readonly Scope _scope;
        private readonly ScopeView _scopeView;
        private readonly AimingService _aimingService;

        public ScopePresenter(Scope scope, ScopeView scopeView, AimingService aimingService)
        {
            _scope = scope;
            _scopeView = scopeView;
            _aimingService = aimingService;
        }

        public void Enable()
        {
            _aimingService.AimEnter += OnAimEnter;
            _aimingService.AimExit += OnAimExit;
        }

        public void Disable()
        {
            _aimingService.AimEnter -= OnAimEnter;
            _aimingService.AimExit -= OnAimExit;
        }

        private void OnAimEnter()
        {
            Camera.main.fieldOfView = 15f;
            _scopeView.Show();
        }

        private void OnAimExit()
        {
            _scopeView.Hide();
        }
    }
}
