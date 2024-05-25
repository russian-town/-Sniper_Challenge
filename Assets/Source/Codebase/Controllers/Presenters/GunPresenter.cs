namespace Source.Root
{
    public class GunPresenter : IPresenter
    {
        private readonly IInput _input;
        private readonly GunView _view;
        private readonly GameLoopService _gameLoopService;

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
