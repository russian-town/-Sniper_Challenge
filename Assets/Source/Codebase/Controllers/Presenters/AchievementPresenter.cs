using UnityEngine;

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

        public async void Enable()
        {
            await _view.ShowAnimation();
            _view.Destroy();
        }

        public void Disable()
        {
        }
    }
}
