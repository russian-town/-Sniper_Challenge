namespace Source.Root
{
    public class GunPresenter : IPresenter
    {
        private IInput _input;
        private GunView _view;
        private GameLoopService _gameLoopService;

        public GunPresenter(IInput input, GunView view, GameLoopService gameLoopService)
        {
            _input = input;
            _view = view;
            _gameLoopService = gameLoopService;
        }

        public void Enable()
        {
            _input.ShootButtonDown += OnShootButtonDown;
        }

        public void Disable()
        {
            _input.ShootButtonDown -= OnShootButtonDown;
        }

        private void OnShootButtonDown()
        {
            _gameLoopService.Shoot(_view.GunEnd);
        }
    }
}
