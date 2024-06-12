using System;
using UnityEngine;

namespace Source.Root
{
    public class GameLoopService
    {
        private readonly IAchievementService _achievementService;
        private readonly AchievementsBoard _achievementsBoard;

        public GameLoopService(
            IAchievementService achievementService,
            AchievementsBoard achievementsBoard)
        {
            _achievementService = achievementService;
            _achievementsBoard = achievementsBoard;
        }

        public event Action AimEnter;
        public event Action AimExit;
        public event Action Shot;
        public event Action<float> CameraRotationChanged;
        public event Action<Transform> SniperShot;
        public event Action SniperDied;

        public void EnterToAim()
            => AimEnter?.Invoke();

        public void ExitOfAim()
            => AimExit?.Invoke();

        public void SniperShoot(Transform point)
        {
            Shot?.Invoke();
            SniperShot?.Invoke(point);
        }

        public void ShowAchievement(AchievementsType achievementsType)
            => _achievementService.FillBoard(_achievementsBoard, achievementsType);

        public void CallEventOfSniperDied()
            => SniperDied?.Invoke();

        public void CallCameraEvent(float angle)
            => CameraRotationChanged?.Invoke(angle);
    }
}
