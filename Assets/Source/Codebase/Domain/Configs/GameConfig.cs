using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "SniperChalleng/Game Config/New Game Config", order = 59)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public CriminalView CriminalViewTemplate { get; private set; }
        [field: SerializeField] public SniperView SniperViewTemplate { get; private set; }
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
