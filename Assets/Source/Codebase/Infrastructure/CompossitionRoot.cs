using Source.Root;
using UnityEngine;

public class CompossitionRoot : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private DesktopInput _desktopInput;

    [Header("Configs")]
    [SerializeField] private LevelConfigs _levelConfigs;

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
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        StaticDataService staticDataService = new();
        ShooterService shooterService = new(_bulletViewTemplate, _levelConfigs.BulletConfig);
        AchievementFactory achievementFactory =
            new(staticDataService, _canvas, new(0f, 540f, 0f));
        GameLoopService gameLoopService = new();
        HudUpdateService hudUpdateService = new();
        staticDataService.LoadConfigs(_levelConfigs);
        hudUpdateService.SetBloodOverlayImage(_bloodOverlay);
        CameraPresenter cameraPresenter =
            new(_cameraView, _desktopInput, _levelConfigs.CameraConfig, _levelConfigs.InputData, gameLoopService, staticDataService);
        _cameraView.Construct(cameraPresenter);
        Gun rifle = new();
        GunPresenter riflePresenter = new(rifle, _rifleView, _levelConfigs.GunConfigs[0]);
        _rifleView.Construct(riflePresenter);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new(40f);
        SniperPresenter sniperPresenter =
            new(sniper,
            _sniperView,
            _desktopInput,
            gameLoopService,
            shooterService,
            hudUpdateService,
            achievementFactory);
        _sniperView.Construct(sniperPresenter);
        HealthBar healthBar = new(sniper.StartHealth);
        HealthBarPresenter healthBarPresenter = new (hudUpdateService, healthBar, _healthBarView);
        _healthBarView.Construct(healthBarPresenter);
        DamageBarFactory damageBarFactory = new(staticDataService);
        Criminal criminal = new(10f);
        CriminalPresenter criminalPresenter =
            new(criminal, _criminalView, gameLoopService, shooterService, damageBarFactory);
        _criminalView.Construct(criminalPresenter);
        Gun pistol = new();
        GunPresenter pistolPresenter = new(pistol, _pistolView, _levelConfigs.GunConfigs[0]);
        _pistolView.Construct(pistolPresenter);
        shooterService.RegistyWeapon(sniper, rifle);
        shooterService.RegistyWeapon(criminal, pistol);
    }
}
