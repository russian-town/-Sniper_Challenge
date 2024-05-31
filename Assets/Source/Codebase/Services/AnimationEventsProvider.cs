using UnityEngine;

namespace Source.Root
{
    public class AnimationEventsProvider : MonoBehaviour
    {
        [SerializeField] private CriminalView _criminalView;

        public void CallShotEvent()
            => _criminalView.CallShotEvent();
    }
}
