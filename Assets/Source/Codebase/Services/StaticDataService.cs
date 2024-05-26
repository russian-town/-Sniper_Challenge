using System;
using System.Collections.Generic;

namespace Source.Root
{
    public class StaticDataService : IStaticDataServis
    {
        private readonly Dictionary<Type, object> _viewTemplatesByType = new();
        private GunConfig[] _gunConfigs;

        public StaticDataService(GunConfig[] gunConfigs)
        {
            _gunConfigs = gunConfigs;
        }

        public float RecoilForce => _gunConfigs[0].RecoilForce;
        public float RifleRange => _gunConfigs[0].Range;
    }
}
