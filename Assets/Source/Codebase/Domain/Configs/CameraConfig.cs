using UnityEngine;

namespace Source.Root
{
    [CreateAssetMenu(fileName = "Camera Config", menuName = "SniperChalleng/Camera Config/New Camera Config", order = 59)]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float MinTitleAngle { get; private set; }
        [field: SerializeField] public float MaxTitleAngle { get; private set; }
        [field: SerializeField] public float MinLookAngle { get; private set; }
        [field: SerializeField] public float MaxLookAngle { get; private set; }
        [field: SerializeField] public float TurnSmooth {  get; private set; }
    }
}
