using UnityEngine;

namespace Source.Root
{
    public class DamageBarFactory
    {
        private readonly IStaticDataServis _staticDataService;

        public DamageBarFactory(IStaticDataServis staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Create(float damage, Vector3 position)
        {
            DamageBar bar = new(damage);
            DamageBarView template = _staticDataService.GetViewTemplate<DamageBarView>();
            DamageBarView view =
                Object.Instantiate(template, position, new Quaternion(0f, 180f, 0f, 0f));
            DamageBarPresenter barPresenter = new(bar, view);
            view.Construct(barPresenter);
        }
    }
}
