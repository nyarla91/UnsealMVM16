namespace Model.Combat.Actions
{
    public abstract class Effect
    {
        public float DelayAfter { get; protected set; }
        
        public Effect(float dealyAfter)
        {
            DelayAfter = dealyAfter;
        }

        public abstract void Execute();
    }
}