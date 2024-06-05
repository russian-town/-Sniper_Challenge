using Source.Root;

public class AchievementService : IAchievementService
{
    private readonly AchievementViewFactory _viewFactory;

    public AchievementService(AchievementViewFactory viewFactory)
    {
        _viewFactory = viewFactory;
    }

    public void FillBoard(AchievementsBoard board, AchievementsType type)
    {
        Achievement achievement = new(type);
        _viewFactory.Create(achievement, board);
    }
}
