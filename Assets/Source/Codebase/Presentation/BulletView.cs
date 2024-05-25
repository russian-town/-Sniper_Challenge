namespace Source.Root
{
    public class BulletView : ViewBase
    {
        private void Update()
        {
            transform.Translate(transform.forward);
        }
    }
}
