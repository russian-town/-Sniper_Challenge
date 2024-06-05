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
            AchievementView view = Object.Instantiate(config.Template);
            AchievementPresenter presenter = new(achievement, view);
            view.Construct(presenter);
            RectTransform parent = board.GetParent();
            view.SetParent(parent);
        }
    }
}
