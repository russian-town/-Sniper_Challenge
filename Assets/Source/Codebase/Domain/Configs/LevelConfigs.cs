using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Level Configs", menuName = "SniperChalleng/Level Configs/New Level Configs", order = 59)]
    public class LevelConfigs : ScriptableObject
    {
        [field: SerializeField] public GunView GunViewTemplate { get; private set; }
        [field: SerializeField] public CriminalView CriminalViewTemplate { get; private set; }
        [field: SerializeField] public BulletView BulletViewTemplate { get; private set; }
        [field: SerializeField] public DamageBarView DamageBarViewTemplate { get; private set; }
        [field: SerializeField] public AchievementView AchievementsView { get; private set; }
        [field: SerializeField] public GunConfig[] GunConfigs { get; private set; }
        [field: SerializeField] public BulletConfig[] BulletConfigs { get; private set; }
        [field: SerializeField] public AchievementConfig[] AchievementsConfigs { get; private set; }
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
        [field: SerializeField] public InputData InputData { get; private set; }
    }
}
