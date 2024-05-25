using Source.Root;
using UnityEngine;

public class CompossitionRoot : MonoBehaviour
{
    [SerializeField] private CoroutineRunner _coroutineRunner;
    [SerializeField] private DesktopInput _desktopInput;
    [SerializeField] private InputConfig _inputConfig;
    [SerializeField] private InputData _inputData;

    [SerializeField] private ScopeView _scopeView;
    [SerializeField] private SniperView _sniperView;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private BulletView _bulletViewTemplate;
    [SerializeField] private GunView _gunView;

    [SerializeField] private BulletViewFactory _bulletViewFactory;

    private void Awake()
    {
        BulletViewFactory bulletViewFactory = new(_bulletViewTemplate, _coroutineRunner);
        GameLoopService gameLoopService = new(bulletViewFactory);
        CameraPresenter cameraPresenter = new(_cameraView, _desktopInput, _inputConfig, _inputData, gameLoopService);
        _cameraView.Construct(cameraPresenter);
        GunPresenter gunPresenter = new(_desktopInput, _gunView, gameLoopService);
        _gunView.Construct(gunPresenter);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService, _coroutineRunner);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new();
        SniperPresenter sniperPresenter = new(sniper, _sniperView, _desktopInput, _coroutineRunner, gameLoopService);
        _sniperView.Construct(sniperPresenter);
    }
}
