using Source.Root;
using UnityEngine;

public class CompossitionRoot : MonoBehaviour
{
    [SerializeField] private DesktopInput _desktopInput;
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [SerializeField] private ScopeView _scopeView;
    [SerializeField] private SniperView _sniperView;

    private void Awake()
    {
        MobileInpute mobileInpute = new();
        AimingService aimingService = new();
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, aimingService);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new();
        SniperPresenter sniperPresenter = new(sniper, _sniperView, _desktopInput, _coroutineRunner, aimingService);
        _sniperView.Construct(sniperPresenter);
    }
}
