using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Bullet Config", menuName = "SniperChalleng/Bullet Config/New Bullet Config", order = 59)]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public BulletType Type { get; private set; }
        [field: SerializeField] public float Damage {  get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}