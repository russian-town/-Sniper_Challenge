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
    [SerializeField] private BulletConfig _bulletConfig;

    [Header("Presentations")]
    [SerializeField] private ScopeView _scopeView;
    [SerializeField] private SniperView _sniperView;
    [SerializeField] private CriminalView _criminalView;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private BulletView _bulletViewTemplate;
    [SerializeField] private GunView _rifleView;
    [SerializeField] private GunView _pistolView;
    [SerializeField] private HealthBarView _healthBarView;

    [Header("UI")]
    [SerializeField] private CanvasGroup _bloodOverlay;
    [SerializeField] private CanvasGroup _skull;

    private void Awake()
    {
        ShooterService shooterService = new(_coroutineRunner, _bulletViewTemplate, _bulletConfig);
        GameLoopService gameLoopService = new();
        StaticDataService staticDataService = new(_gunConfigs);
        HudUpdateService hudUpdateService = new();
        hudUpdateService.SetBloodOverlayImage(_bloodOverlay);
        hudUpdateService.SetSkullImage(_skull);
        CameraPresenter cameraPresenter =
            new(_cameraView, _desktopInput, _cameraConfig, _inputData, gameLoopService, staticDataService);
        _cameraView.Construct(cameraPresenter);
        Gun rifle = new();
        GunPresenter riflePresenter = new(rifle, _rifleView, _gunConfigs[0]);
        _rifleView.Construct(riflePresenter);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new(40f);
        SniperPresenter sniperPresenter =
            new(sniper, _sniperView, _desktopInput, _coroutineRunner, gameLoopService, shooterService, hudUpdateService);
        _sniperView.Construct(sniperPresenter);
        HealthBar healthBar = new HealthBar(sniper.StartHealth);
        HealthBarPresenter healthBarPresenter = new HealthBarPresenter(hudUpdateService, healthBar, _healthBarView);
        _healthBarView.Construct(healthBarPresenter);
        Criminal criminal = new(10f);
        CriminalPresenter criminalPresenter =
            new(criminal, _criminalView, gameLoopService, shooterService, _coroutineRunner, hudUpdateService);
        _criminalView.Construct(criminalPresenter);
        Gun pistol = new();
        GunPresenter pistolPresenter = new(pistol, _pistolView, _gunConfigs[0]);
        _pistolView.Construct(pistolPresenter);
        shooterService.RegistyWeapon(sniper, rifle);
        shooterService.RegistyWeapon(criminal, pistol);
    }
}
