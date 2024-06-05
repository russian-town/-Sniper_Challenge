namespace Source.Root
{
    public class AchievementBoardPresenter : IPresenter
    {
        private readonly AchievementsBoard _board;
        private readonly AchievementBoardView _view;

        public AchievementBoardPresenter(
            AchievementsBoard board,
            AchievementBoardView view)
        {
            _board = board;
            _view = view;
            _board.SetParent(_view.Container);
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
