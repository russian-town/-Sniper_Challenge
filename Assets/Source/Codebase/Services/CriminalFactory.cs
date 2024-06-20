using Source.Codebase.Domain;
using Source.Codebase.Domain.Configs;
using Source.Root;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class CriminalFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly GameLoopService _gameLoopService;
        private readonly GunFactory _gunFactory;
        private readonly Transform _target;

        public CriminalFactory(
            IStaticDataService staticDataService,
            GameLoopService gameLoopService,
            GunFactory gunFactory,
            Transform target)
        {
            _staticDataService = staticDataService;
            _gameLoopService = gameLoopService;
            _gunFactory = gunFactory;
            _target = target;
        }

        public void Create(CriminalLevelConfig[] levelConfigs)
        {
            foreach (var levelConfig in levelConfigs)
            {
                DamageBarFactory damageBarFactory = new(_staticDataService);
                Criminal criminal = new(10f);
                CriminalView criminalViewTemplate = levelConfig.CriminalViewTemplate;
                CriminalView criminalView =
                    Object.Instantiate(
                        criminalViewTemplate,
                        levelConfig.SpawnPoint.position,
                        levelConfig.SpawnPoint.rotation);
                GunConfig gunConfig =
                    _staticDataService.GetGunConfig(GunType.Pistol);
                CriminalPresenter criminalPresenter =
                    new(criminal,
                    criminalView,
                    _target,
                    _gameLoopService,
                    damageBarFactory,
                    _gunFactory,
                    gunConfig);
                criminalView.Construct(criminalPresenter);
            }
        }
    }
}
