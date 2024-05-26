using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Rifle Config", menuName = "SniperChalleng/Rifle Config/New Rifle Config", order = 59)]
    public class RifleConfig : ScriptableObject
    {
        [field: SerializeField] public float RecoilForce { get; private set; }
        [field: SerializeField] public float ReloadingSpeed { get; private set; }
        [field: SerializeField] public float AccuracyFromHip { get; private set; }
        [field: SerializeField] public float AimSpeed { get; private set; }
        [field: SerializeField] public Sprite ScopeSprite { get; private set; }
    }
}
