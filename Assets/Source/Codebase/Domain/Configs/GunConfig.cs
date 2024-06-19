using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Gun Config", menuName = "SniperChalleng/Gun Config/New Gun Config", order = 59)]
    public class GunConfig : ScriptableObject
    {
        [field: SerializeField] public float RecoilForce { get; private set; }
        [field: SerializeField] public float Range { get; private set; }
        [field: SerializeField] public float ReloadingSpeed { get; private set; }
        [field: SerializeField] public int BulletCountInMagazine { get; private set; }
        [field: SerializeField] public float AccuracyFromHip { get; private set; }
        [field: SerializeField] public float AimSpeed { get; private set; }
        [field: SerializeField] public Sprite ScopeSprite { get; private set; }
        [field: SerializeField] public BulletType BulletType { get; private set; }
        [field: SerializeField] public GunType Type { get; private set; }
        [field: SerializeField] public GunView Template { get; private set; }
        [field: SerializeField] public Quaternion LocalRotation { get; private set; }
        [field: SerializeField] public Vector3 LocalPosition { get; private set;}
    }
}
