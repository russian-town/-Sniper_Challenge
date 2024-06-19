using System;
using UnityEngine;

namespace Source.Utils
{
    [Serializable]
    public class HumaneBone
    {
        [SerializeField] [Range(0f, 1f)] private float _wight;

        [field: SerializeField] public HumanBodyBones Bone { get; private set; }
        public float Wight => _wight;
    }
}
