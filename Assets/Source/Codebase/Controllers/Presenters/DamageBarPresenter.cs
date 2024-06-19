namespace Source.Root
{
    public class DamageBarPresenter : IPresenter
    {
        private readonly DamageBar _bar;
        private readonly DamageBarView _view;

        public DamageBarPresenter(DamageBar bar, DamageBarView view)
        {
            _bar = bar;
            _view = view;
        }

        public async void Enable() 
        {
            _view.SetDamageText(_bar.DamageText);
            await _view.ShotAnimation();
            _view.Destroy();
        }

        public void LateUpdate(float tick) { }

        public void Disable() { }
    }
}
