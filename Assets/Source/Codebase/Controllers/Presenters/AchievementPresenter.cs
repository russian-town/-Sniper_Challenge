namespace Source.Root
{
    public class AchievementPresenter : IPresenter
    {
        private readonly Achievement _achievement;
        private readonly AchievementView _view;

        public AchievementPresenter(Achievement achievement, AchievementView view)
        {
            _achievement = achievement;
            _view = view;
            _view.ResetAnimation();
        }

        public void Enable() { }

        public void LateUpdate(float tick) { }

        public void Disable() { }
    }
}
