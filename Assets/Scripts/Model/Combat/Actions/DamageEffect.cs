using Model.Combat.Characters;

namespace Model.Combat.Actions
{
    public class DamageEffect : Effect
    {
        private readonly Character _character;
        private readonly int _damage;

        public DamageEffect(Character character, int damage, float dealyAfter) : base(dealyAfter)
        {
            _character = character;
            _damage = damage;
        }

        public override void Execute()
        {
            _character.DealDamage(_damage);
        }
    }
}