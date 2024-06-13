using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Root
{
    public class AchievementFactory
    {
        private readonly IStaticDataServis _staticDataServis;
        private readonly Canvas _canvas;
        private readonly Vector3 _localPosition;
        private readonly Queue<AchievementsType> _achievements;
        
        private bool _inProcess = false;

        public AchievementFactory(
            IStaticDataServis staticDataServis,
            Canvas canvas,
            Vector3 localPosition)
        {
            _staticDataServis = staticDataServis;
            _canvas = canvas;
            _localPosition = localPosition;
            _achievements = new();
        }

        public async void Create(AchievementsType type) 
        {
            _achievements.Enqueue(type);

            await UniTask.WaitUntil(() => _inProcess == false);

            while (_achievements.Count > 0)
            {
                _inProcess = true;
                AchievementsType typeInQueue = _achievements.Dequeue();
                Achievement achievement = new(typeInQueue);
                AchievementConfig config =
                    _staticDataServis.GetAchievementConfig(typeInQueue);
                AchievementView template = _staticDataServis.GetViewTemplate<AchievementView>();
                AchievementView view = Object.Instantiate(template, _canvas.transform);
                AchievementPresenter presenter = new(achievement, view);
                view.SetSprite(config.Sprite);
                view.Construct(presenter);
                view.SetPosition(_localPosition);
                await view.ShowAnimation();
            }

            _inProcess = false;
        }
    }
}
