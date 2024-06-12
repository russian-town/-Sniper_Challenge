namespace Source.Root
{
    public interface IBodyPart
    {
        public BodyPartName Name { get; }
        public CriminalView CriminalView { get; }

        public bool CheckDead(float damage);
    }
}
