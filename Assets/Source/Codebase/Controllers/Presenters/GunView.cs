using UnityEngine;

namespace Source.Root
{
    public class GunView : ViewBase
    {
        [field: SerializeField] public Transform GunEnd {  get; private set; }
    }
}