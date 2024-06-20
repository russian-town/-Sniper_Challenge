using System;
using Source.Root;
using UnityEngine;

namespace Source.Codebase.Domain.Configs
{
    [Serializable]
    public class CriminalLevelConfig
    {
        [field: SerializeField] public Transform SpawnPoint { get; private set; }
        [field: SerializeField] public CriminalView CriminalViewTemplate { get; private set; }
    }
}
