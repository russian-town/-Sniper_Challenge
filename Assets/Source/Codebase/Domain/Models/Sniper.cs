namespace Source.Root
{
    public class Sniper : Character
    {
        public bool InAim { get; private set; }

        public void EnterToAim()
            => InAim = true;

        public void ExitOfAim()
            => InAim = false;
    }
}
