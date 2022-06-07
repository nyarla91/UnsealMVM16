using Model.Cards.Spells;

namespace Model.Combat.Effects
{
    public class RemoveChargesFromSpellEffect : Effect
    {
        private readonly Spell _spell;
        private readonly int _charges;

        public RemoveChargesFromSpellEffect(float dealyAfter, Spell spell, int charges) : base(dealyAfter)
        {
            _spell = spell;
            _charges = charges;
        }

        public override void Execute()
        {
            if (_spell == null)
                return;
            
            _spell.Charges -= _charges;
        }
    }
}