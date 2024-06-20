using Source.Root;
using UnityEngine;

namespace Source.Codebase.Services
{
    public class SniperFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IInput _input;
        private readonly GameLoopService _gameLoopService;
        private readonly HudUpdateService _hudUpdateService;
        private readonly AchievementFactory _achievementFactory;
        private readonly GunFactory _gunFactory;

        public SniperFactory(
            IStaticDataService staticDataService,
            IInput input,
            GameLoopService gameLoopService,
            HudUpdateService hudUpdateService,
            AchievementFactory achievementFactory,
            GunFactory gunFactory)
        {
            _staticDataService = staticDataService;
            _input = input;
            _gameLoopService = gameLoopService;
            _hudUpdateService = hudUpdateService;
            _achievementFactory = achievementFactory;
            _gunFactory = gunFactory;
        }

        public void Create(Transform spawnPoint, float health)
        {
            Sniper sniper = new(health);
            SniperView sniperViewTemplate =
                _staticDataService.GetViewTemplate<SniperView>();
            SniperView sniperView =
                Object.Instantiate(
                    sniperViewTemplate,
                    spawnPoint.position,
                    spawnPoint.rotation);
            SniperPresenter sniperPresenter =
                new(sniper,
            sniperView,
                _staticDataService,
                _input,
                _gameLoopService,
                _hudUpdateService,
                _achievementFactory,
                _gunFactory);
            sniperView.Construct(sniperPresenter);
        }
    }
}
