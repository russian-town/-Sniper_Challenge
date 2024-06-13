namespace Source.Root
{
    public class Bullet
    {
        public Bullet(BulletConfig config)
        {
            Damage = config.Damage;
            Speed = config.Speed;
        }

        public float Damage { get; private set; }
        public float Speed { get; private set; }
    }
}
