using Source.Root;
using UnityEngine;

public class CompossitionRoot : MonoBehaviour
{
    [Header("Services")]
    [SerializeField] private CoroutineRunner _coroutineRunner;

    [Header("Input")]
    [SerializeField] private DesktopInput _desktopInput;

    [Header("Configs")]
    [SerializeField] private CameraConfig _cameraConfig;
    [SerializeField] private RifleConfig[] _rifleConfigs;
    [SerializeField] private InputData _inputData;

    [Header("Presentations")]
    [SerializeField] private ScopeView _scopeView;
    [SerializeField] private SniperView _sniperView;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private BulletView _bulletViewTemplate;
    [SerializeField] private GunView _gunView;

    [Header("Factories")]
    [SerializeField] private BulletViewFactory _bulletViewFactory;

    private void Awake()
    {
        BulletViewFactory bulletViewFactory = new(_bulletViewTemplate, _coroutineRunner);
        GameLoopService gameLoopService = new(bulletViewFactory);
        StaticDataService staticDataService = new(_rifleConfigs);
        CameraPresenter cameraPresenter =
            new(_cameraView, _desktopInput, _cameraConfig, _inputData, gameLoopService, staticDataService);
        _cameraView.Construct(cameraPresenter);
        GunPresenter gunPresenter = new(_desktopInput, _gunView, gameLoopService);
        _gunView.Construct(gunPresenter);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new();
        SniperPresenter sniperPresenter = new(sniper, _sniperView, _desktopInput, _coroutineRunner, gameLoopService);
        _sniperView.Construct(sniperPresenter);
    }
}
