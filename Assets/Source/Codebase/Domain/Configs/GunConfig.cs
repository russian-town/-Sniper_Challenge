using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Gun Config", menuName = "SniperChalleng/Gun Config/New Gun Config", order = 59)]
    public class GunConfig : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float RecoilForce { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float ReloadingSpeed { get; private set; }
        [field: SerializeField] public float AccuracyFromHip { get; private set; }
        [field: SerializeField] public float AimSpeed { get; private set; }
        [field: SerializeField] public Sprite ScopeSprite { get; private set; }
    }
}
