namespace Source.Root
{
    public class Sniper : Character
    {
        public Sniper(float health) : base(health)
        {
        }

        public bool InAim { get; private set; }

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;
    }
}
