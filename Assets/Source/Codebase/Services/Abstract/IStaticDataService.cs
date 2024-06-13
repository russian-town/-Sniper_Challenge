using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Root
{
    public interface IStaticDataService
    {
        public AchievementConfig GetAchievementConfig(AchievementsType achievementsType);
        public BulletConfig GetBulletConfig(BulletType bulletType);
        public GunConfig GetGunConfig(GunType gunType);
        public T GetViewTemplate<T>() where T : MonoBehaviour;
    }
}