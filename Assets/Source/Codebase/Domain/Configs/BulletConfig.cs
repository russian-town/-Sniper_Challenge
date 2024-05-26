using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Bullet Config", menuName = "SniperChalleng/Bullet Config/New Bullet Config", order = 59)]
    public class BulletConfig : ScriptableObject
    {
        [field: SerializeField] public float Damage {  get; private set; }
    }
}