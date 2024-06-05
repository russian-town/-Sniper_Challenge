namespace Source.Root
{
    public class Achievement
    {
        public Achievement(AchievementsType type)
        {
            Type = type;
        }

        public AchievementsType Type { get; private set; }
    }
}
