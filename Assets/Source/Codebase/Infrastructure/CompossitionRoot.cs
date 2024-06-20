using Source.Codebase.Services;
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
    [SerializeField] private HealthBarView _healthBarView;

    [Header("UI")]
    [SerializeField] private CanvasGroup _bloodOverlay;
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        StaticDataService staticDataService = new();
        GameLoopService gameLoopService = new();
        HudUpdateService hudUpdateService = new();
        BulletFactory bulletFactory = new(staticDataService);
        GunFactory gunFactory = new(bulletFactory);
        AchievementFactory achievementFactory =
            new(staticDataService, _canvas, new(0f, 540f, 0f));
        staticDataService.LoadConfigs(_levelConfigs);
        hudUpdateService.SetBloodOverlayImage(_bloodOverlay);
        CameraPresenter cameraPresenter =
            new(_cameraView,
            _desktopInput,
            _levelConfigs.CameraConfig,
            _levelConfigs.InputData,
            gameLoopService,
            staticDataService);
        _cameraView.Construct(cameraPresenter);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new(40f);
        SniperPresenter sniperPresenter =
            new(sniper,
            _sniperView,
            staticDataService,
            _desktopInput,
            gameLoopService,
            hudUpdateService,
            achievementFactory,
            gunFactory);
        _sniperView.Construct(sniperPresenter);
        HealthBar healthBar = new(sniper.StartHealth);
        HealthBarPresenter healthBarPresenter = new (hudUpdateService, healthBar, _healthBarView);
        _healthBarView.Construct(healthBarPresenter);
        DamageBarFactory damageBarFactory = new(staticDataService);
        Criminal criminal = new(10f);
        CriminalPresenter criminalPresenter =
            new(criminal,
            _criminalView,
            _sniperView.TargetOfCriminal,
            staticDataService,
            gameLoopService,
            damageBarFactory,
            gunFactory);
        _criminalView.Construct(criminalPresenter);
    }
}
