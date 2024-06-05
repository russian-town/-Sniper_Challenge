using UnityEngine;

namespace Source.Root
{
    public class AchievementsBoard
    {
        private RectTransform _parent;

        public void SetParent(RectTransform parent)
            => _parent = parent;

        public RectTransform GetParent()
            => _parent;
    }
}
