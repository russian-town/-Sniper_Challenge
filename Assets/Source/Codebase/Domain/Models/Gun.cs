namespace Source.Root
{
    public class Gun
    {
        public Gun(GunConfig gunConfig)
        {
            Range = gunConfig.Range;
        }

        public float Range { get; private set; }
    }
}
