using UnityEngine;

namespace Source.Root
{
    public interface IGun
    {
        public Transform EndPoint { get; }
        public float Range { get; }
    }
}
