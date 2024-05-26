using System;
using System.Collections.Generic;

namespace Source.Root
{
    public class StaticDataService : IStaticDataServis
    {
        private readonly Dictionary<Type, object> _viewTemplatesByType = new();
        private RifleConfig[] _rifleConfigs;

        public StaticDataService(RifleConfig[] rifleConfigs)
        {
            _rifleConfigs = rifleConfigs;
        }

        public float RecoilForce => _rifleConfigs[0].RecoilForce;
    }
}
