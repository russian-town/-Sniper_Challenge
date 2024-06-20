using System;
using System.Collections.Generic;
using System.Linq;
using Source.Codebase.Domain;
using UnityEngine;

namespace Source.Root
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, object> _viewTemplateByTypes;
        
        private Dictionary<AchievementsType, AchievementConfig> _achivementConfigByType;
        private Dictionary<BulletType, BulletConfig> _bulletConfigByType;
        private Dictionary<GunType, GunConfig> _gunConfigByType;

        public StaticDataService()
        {
            _viewTemplateByTypes = new();
            _achivementConfigByType = new();
            _bulletConfigByType = new();
        }

        public void LoadConfigs(GameConfig gameConfig)
        {
            LoadAchievementConfigs(gameConfig.AchievementsConfigs);
            LoadBulletConfigs(gameConfig.BulletConfigs);
            LoadGunConfigs(gameConfig.GunConfigs);

            _viewTemplateByTypes.Clear();
            _viewTemplateByTypes.Add(typeof(SniperView), gameConfig.SniperViewTemplate);
            _viewTemplateByTypes.Add(typeof(BulletView), gameConfig.BulletViewTemplate);
            _viewTemplateByTypes.Add(typeof(DamageBarView), gameConfig.DamageBarViewTemplate);
            _viewTemplateByTypes.Add(typeof(AchievementView), gameConfig.AchievementsView);
        }

        public T GetViewTemplate<T>() where T : MonoBehaviour
        {
            if (_viewTemplateByTypes.TryGetValue(typeof(T), out object viewTemplate))
                return viewTemplate as T;

            throw new Exception($"Can't find viewTemplate with given type: {typeof(T)} ");
        }

        public AchievementConfig GetAchievementConfig(AchievementsType achievementsType)
        {
            if (_achivementConfigByType.ContainsKey(achievementsType) == false)
                throw new Exception($"AchivementConfig for AchievementsType {achievementsType} does not exist!");

            return _achivementConfigByType[achievementsType];
        }

        public BulletConfig GetBulletConfig(BulletType bulletType)
        {
            if (_bulletConfigByType.ContainsKey(bulletType) == false)
                throw new Exception($"BulletConfig for BulletType {bulletType} does not exist!");

            return _bulletConfigByType[bulletType];
        }

        public GunConfig GetGunConfig(GunType gunType)
        {
            if (_gunConfigByType.ContainsKey(gunType) == false)
                throw new Exception($"GunConfig for GunType {gunType} does not exist!");

            return _gunConfigByType[gunType];
        }

        private void LoadAchievementConfigs(AchievementConfig[] configs)
        {
            if (configs.Length != configs.Distinct().Count())
                throw new Exception("All achievementConfig must be distinct");

            _achivementConfigByType =
                configs.ToDictionary(achivementConfig => achivementConfig.Type, achivementConfig => achivementConfig);
        }

        private void LoadBulletConfigs(BulletConfig[] configs)
        {
            if (configs.Length != configs.Distinct().Count())
                throw new Exception("All achievementConfig must be distinct");

            _bulletConfigByType =
                configs.ToDictionary(bulletConfig => bulletConfig.Type, bulletConfig => bulletConfig);
        }

        private void LoadGunConfigs(GunConfig[] configs)
        {
            if (configs.Length != configs.Distinct().Count())
                throw new Exception("All achievementConfig must be distinct");

            _gunConfigByType =
                 configs.ToDictionary(gunConfig => gunConfig.Type, gunConfig => gunConfig);
        }
    }
}
