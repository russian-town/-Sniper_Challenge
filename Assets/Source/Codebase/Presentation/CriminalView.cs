using UnityEngine;

namespace Source.Root
{
    public class CriminalView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody[] _bodyParts;

        public void Initialize()
            => TougleRagdoll(false);

        public void Die()
        {
            TougleRagdoll(true);
        }

        public void TougleRagdoll(bool enable)
        {
            foreach (var rigidbody in _bodyParts)
            {
                rigidbody.isKinematic = !enable;
            }
        }
    }
}
