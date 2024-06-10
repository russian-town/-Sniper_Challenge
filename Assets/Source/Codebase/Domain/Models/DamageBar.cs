namespace Source.Root
{
    public class DamageBar
    {
        public string DamageText { get; private set; }

        public DamageBar(float damage)
        {
            DamageText = damage.ToString();
        }
    }
}
