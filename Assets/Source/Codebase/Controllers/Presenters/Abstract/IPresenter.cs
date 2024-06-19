namespace Source.Root
{
    public interface IPresenter
    {
        public void Enable();

        public void LateUpdate(float tick);

        public void Disable();
    }
}
