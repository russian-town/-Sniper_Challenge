namespace Source.Root
{
    public class HealthBarPresenter : IPresenter
    {
        private readonly HudUpdateService _updateService;
        private readonly HealthBar _healthBar;
        private readonly HealthBarView _healthBarView;

        public HealthBarPresenter(
            HudUpdateService updateService,
            HealthBar healthBar,
            HealthBarView healthBarView)
        {
            _updateService = updateService;
            _healthBar = healthBar;
            _healthBarView = healthBarView;

        }

        public void Enable()
        {
            _updateService.HealthChanged += OnHealthChanged;
            _healthBar.ValueUpdated += OnValueUpdated;
        }

        public void LateUpdate(float tick) { }

        public void Disable()
        {
            _updateService.HealthChanged -= OnHealthChanged;
            _healthBar.ValueUpdated -= OnValueUpdated;
        }

        private void OnHealthChanged(float value)
            => _healthBar.UpdateValue(value);

        private void OnValueUpdated(float normalizeValue)
            => _healthBarView.UpdateValue(normalizeValue);
    }
}
