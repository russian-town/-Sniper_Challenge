using UnityEngine;

namespace Source.Codebase.Domain
{
    public class DebugDrawSphere : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
        }
    }
}
