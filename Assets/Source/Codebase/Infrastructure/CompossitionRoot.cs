using Source.Codebase.Data;
using Source.Codebase.Domain.Configs;
using Source.Codebase.Services;
using Source.Root;
using UnityEngine;

public class CompossitionRoot : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private DesktopInput _desktopInput;

    [Header("Configs")]
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private CriminalLevelConfig[] _criminalLevelConfigs;

    [Header("Presentations")]
    [SerializeField] private ScopeView _scopeView;
    [SerializeField] private CameraView _cameraView;
    [SerializeField] private BulletView _bulletViewTemplate;
    [SerializeField] private HealthBarView _healthBarView;

    [Header("UI")]
    [SerializeField] private CanvasGroup _bloodOverlay;
    [SerializeField] private Canvas _canvas;

    [Header("Scene Objects")]
    [SerializeField] private Transform _sniperSpawnPoint;

    private void Awake()
    {
        StaticDataService staticDataService = new();
        GameLoopService gameLoopService = new();
        HudUpdateService hudUpdateService = new();
        CameraPresenter cameraPresenter =
           new(_cameraView,
           _desktopInput,
           _gameConfig.CameraConfig,
           _gameConfig.InputData,
           gameLoopService,
           staticDataService);
        _cameraView.Construct(cameraPresenter);
        BulletFactory bulletFactory = new(staticDataService);
        GunFactory gunFactory = new(bulletFactory);
        AchievementFactory achievementFactory =
            new(staticDataService, _canvas, new(0f, 540f, 0f));
        staticDataService.LoadConfigs(_gameConfig);
        hudUpdateService.SetBloodOverlayImage(_bloodOverlay);
        SniperData sniperData = new();
        SniperFactory sniperFactory =
            new(staticDataService,
                _desktopInput,
                gameLoopService,
                hudUpdateService,
                achievementFactory,
                gunFactory);
        sniperFactory.Create(_sniperSpawnPoint, sniperData.Health);
        CriminalFactory criminalFactory =
            new(staticDataService,
            gameLoopService,
            gunFactory,
            _sniperSpawnPoint);
        criminalFactory.Create(_criminalLevelConfigs);
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService);
        _scopeView.Construct(scopePresenter);
        HealthBar healthBar = new(sniperData.Health);
        HealthBarPresenter healthBarPresenter = new(hudUpdateService, healthBar, _healthBarView);
        _healthBarView.Construct(healthBarPresenter);
    }
}
