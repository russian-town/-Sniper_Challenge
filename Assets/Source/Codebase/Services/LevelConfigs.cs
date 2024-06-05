using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Level Configs", menuName = "SniperChalleng/Level Configs/New Level Configs", order = 59)]
    public class LevelConfigs : ScriptableObject
    {
        [field: SerializeField] public object GunViewTemplate { get; private set; }
        [field: SerializeField] public object CriminalViewTemplate { get; private set; }
        [field: SerializeField] public object BulletViewTemplate { get; private set; }
        [field: SerializeField] public object AchievementViewTemplate { get; private set; }
        [field: SerializeField] public GunConfig[] GunConfigs { get; private set; }
        [field: SerializeField] public InputData InputData { get; private set; }
        [field: SerializeField] public BulletConfig BulletConfig { get; private set; }
        [field: SerializeField] public AchievementConfig[] AchievementsConfigs { get; private set; }
        [field: SerializeField] public CameraConfig CameraConfig { get; private set; }
    }
}
