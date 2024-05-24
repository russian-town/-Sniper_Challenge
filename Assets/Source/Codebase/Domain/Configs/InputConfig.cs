using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Input Config", menuName = "Input Config/New Input Config", order = 59)]
    public class InputConfig : ScriptableObject
    {
        [field: SerializeField] public float MinTitleAngle { get; private set; }
        [field: SerializeField] public float MaxTitleAngle { get; private set; }
        [field: SerializeField] public float MinLookAngle { get; private set; }
        [field: SerializeField] public float MaxLookAngle { get; private set; }
        [field: SerializeField] public float TurnSmooth {  get; private set; }
    }
}
