using UnityEngine;

namespace Source.Root
{
    public class AchievementViewFactory
    {
        private readonly IStaticDataServis _staticDataServis;

        private AchievementView _currentAchievementView;

        public AchievementViewFactory(IStaticDataServis staticDataServis)
        {
            _staticDataServis = staticDataServis;
        }

        public void Create(Achievement achievement, AchievementsBoard board) 
        {
            if(_currentAchievementView != null)
                _currentAchievementView.CancelAnimation();

            AchievementConfig config =
                _staticDataServis.GetAchievementConfig(achievement.Type);
            AchievementView template = _staticDataServis.GetViewTemplate<AchievementView>();
            AchievementView view = Object.Instantiate(template);
            _currentAchievementView = view;
            AchievementPresenter presenter = new(achievement, view);
            view.SetSprite(config.Sprite);
            view.Construct(presenter);
            RectTransform parent = board.GetParent();
            view.SetParent(parent);
        }
    }
}
