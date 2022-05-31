using Model.Combat.Characters;

namespace Model.Combat.Effects
{
    public class TriggerPereodicDamageEffect : Effect
    {
        private readonly Character _target;

        public TriggerPereodicDamageEffect(float dealyAfter, Character target) : base(dealyAfter)
        {
            _target = target;
        }

        public override void Execute()
        {
            if (_target == null)
                return;

            _target.TriggerPereodicDamage();
        }
    }
}