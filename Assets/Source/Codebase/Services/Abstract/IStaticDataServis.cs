using UnityEngine;

namespace Source.Root
{
    public interface IStaticDataServis
    {
        AchievementConfig GetAchievementConfig(AchievementsType achievementsType);
        public T GetViewTemplate<T>() where T : MonoBehaviour;
    }
}