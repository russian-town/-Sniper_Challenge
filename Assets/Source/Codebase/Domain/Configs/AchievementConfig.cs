using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Achievement Config", menuName = "SniperChalleng/Achievement Config/New Achievement Config", order = 59)]
    public class AchievementConfig : ScriptableObject
    {
        [field: SerializeField] public AchievementView Template { get; private set; }
        [field: SerializeField] public float Score { get; private set; }
        [field: SerializeField] public AchievementsType Type { get; private set; }
    }
}
