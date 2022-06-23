namespace Model.Combat.Effects
{
    public abstract class Effect
    {
        public float DelayAfter { get; protected set; }
        public virtual string[] Sounds { get; }

        protected Effect(float dealyAfter)
        {
            DelayAfter = dealyAfter;
        }

        public abstract void Execute();
    }
}