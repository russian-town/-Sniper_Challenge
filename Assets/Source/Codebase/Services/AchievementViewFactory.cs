using UnityEngine;

namespace Source.Root
{
    public class AchievementViewFactory
    {
        private readonly IStaticDataServis _staticDataServis;

        public AchievementViewFactory(IStaticDataServis staticDataServis)
        {
            _staticDataServis = staticDataServis;
        }

        public void Create(Achievement achievement, AchievementsBoard board) 
        {
            AchievementConfig config =
                _staticDataServis.GetAchievementConfig(achievement.Type);
            AchievementView template = _staticDataServis.GetViewTemplate<AchievementView>();
            AchievementView view = Object.Instantiate(template);
            AchievementPresenter presenter = new(achievement, view);
            view.SetSprite(config.Sprite);
            view.Construct(presenter);
            RectTransform parent = board.GetParent();
            view.SetParent(parent);
        }
    }
}
