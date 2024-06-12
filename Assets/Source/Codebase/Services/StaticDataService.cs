using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Root
{
    public class StaticDataService : IStaticDataServis
    {
        private readonly Dictionary<Type, object> _viewTemplateByTypes;
        
        private Dictionary<AchievementsType, AchievementConfig> _achivementConfigByType;
        private GunConfig[] _gunConfigs;

        public StaticDataService()
        {
            _viewTemplateByTypes = new();
            _achivementConfigByType = new();
        }

        public float RecoilForce => _gunConfigs[0].RecoilForce;

        public void LoadConfigs(LevelConfigs levelConfig)
        {
            LoadAchievementConfigs(levelConfig.AchievementsConfigs);
            LoadGunConfigs(levelConfig.GunConfigs);

            _viewTemplateByTypes.Clear();
            _viewTemplateByTypes.Add(typeof(GunView), levelConfig.GunViewTemplate);
            _viewTemplateByTypes.Add(typeof(CriminalView), levelConfig.CriminalViewTemplate);
            _viewTemplateByTypes.Add(typeof(BulletView), levelConfig.BulletViewTemplate);
            _viewTemplateByTypes.Add(typeof(DamageBarView), levelConfig.DamageBarViewTemplate);
            _viewTemplateByTypes.Add(typeof(AchievementView), levelConfig.AchievementsView);
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

        private void LoadAchievementConfigs(AchievementConfig[] configs)
        {
            if (configs.Length != configs.Distinct().Count())
                throw new Exception("All achievementConfig must be distinct");

            _achivementConfigByType =
                configs.ToDictionary(achivementConfig => achivementConfig.Type, achivementConfig => achivementConfig);
        }

        private void LoadGunConfigs(GunConfig[] gunConfigs)
        {
            _gunConfigs = gunConfigs;
        }
    }
}
