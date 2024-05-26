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
    [SerializeField] private GunConfig[] _gunConfigs;
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
        StaticDataService staticDataService = new(_gunConfigs);
        CameraPresenter cameraPresenter =
            new(_cameraView, _desktopInput, _cameraConfig, _inputData, gameLoopService, staticDataService);
        _cameraView.Construct(cameraPresenter);
        Gun gun = new();
        GunPresenter gunPresenter =
            new(_desktopInput, gun, _gunView, gameLoopService, _gunConfigs[0]);
        _gunView.Construct(gunPresenter);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new(40f);
        SniperPresenter sniperPresenter = new(sniper, _sniperView, _desktopInput, _coroutineRunner, gameLoopService);
        _sniperView.Construct(sniperPresenter);
    }
}
