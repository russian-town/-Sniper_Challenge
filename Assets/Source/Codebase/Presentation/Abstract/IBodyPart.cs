namespace Source.Root
{
    public interface IBodyPart
    {
        public BodyPartName Name { get; }

        public bool CheckDead(float damage);
    }
}
