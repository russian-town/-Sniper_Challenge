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

    private void Awake()
    {
        CameraPresenter cameraPresenter = new(_cameraView, _desktopInput, _inputConfig, _inputData);
        _cameraView.Construct(cameraPresenter);
        GameLoopService gameLoopService = new();
        Scope scope = new();
        ScopePresenter scopePresenter = new(scope, _scopeView, gameLoopService, _coroutineRunner);
        _scopeView.Construct(scopePresenter);
        Sniper sniper = new();
        SniperPresenter sniperPresenter = new(sniper, _sniperView, _desktopInput, _coroutineRunner, gameLoopService);
        _sniperView.Construct(sniperPresenter);
    }
}
