using Model.Cards.Spells;

namespace Model.Combat.Effects
{
    public class AddChargesToSpellEffect : Effect
    {
        private readonly Spell _spell;
        private readonly int _charges;
        public override string[] Sounds => new []{"Effects/Charge1", "Effects/Charge2","Effects/Charge3", "Effects/Charge4"};

        public AddChargesToSpellEffect(float dealyAfter, Spell spell, int charges) : base(dealyAfter)
        {
            _spell = spell;
            _charges = charges;
        }

        public override void Execute()
        {
            if (_spell == null)
                return;
            _spell.Charges += _charges;
        }
    }
}